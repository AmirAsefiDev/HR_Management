using FluentValidation;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveRequest.CreateLeaveRequest;

namespace HR_Management.Application.DTOs.LeaveRequest.Validators;

public class CreateLeaveRequestDtoValidator : AbstractValidator<CreateLeaveRequestDto>
{
    private readonly ILeaveStatusRepository _leaveStatusRepo;
    private readonly ILeaveTypeRepository _leaveTypeRepo;

    public CreateLeaveRequestDtoValidator(ILeaveTypeRepository leaveTypeRepo, ILeaveStatusRepository leaveStatusRepo)
    {
        _leaveTypeRepo = leaveTypeRepo;
        _leaveStatusRepo = leaveStatusRepo;
        Include(new ILeaveRequestDtoValidator(_leaveTypeRepo, _leaveStatusRepo));

        RuleFor(l => l.LeaveMeasureType)
            .IsInEnum().WithMessage("{PropertyName} must be valid LeaveMeasureType.1 = DayBased,2 = HourBased");

        RuleFor(l => l)
            .CustomAsync(async (dto, context, cancellationToken) =>
            {
                if (dto.LeaveMeasureType == LeaveMeasureType.DayBased)
                    if (dto.StartDate.Date > dto.EndDate.Date)
                        context.AddFailure("EndDate", "EndDate must be after StartDate for day-based leave.");

                if (dto.LeaveMeasureType == LeaveMeasureType.HourBased)
                {
                    if (dto.StartDate >= dto.EndDate)
                        context.AddFailure("EndDate", "EndDate must be after StartDate for hour-based leave.");

                    if (dto.StartDate.Date != dto.EndDate.Date)
                    {
                        context.AddFailure("For hour-based leave, StartDate and EndDate must be on the same day.");
                        return;
                    }

                    var hoursRequested = (dto.EndDate - dto.StartDate).TotalHours;
                    var leaveType = await _leaveTypeRepo.GetAsync(dto.LeaveTypeId);
                    if (leaveType == null)
                    {
                        context.AddFailure("Invalid leave type.");
                        return;
                    }

                    if (hoursRequested > leaveType.HoursPerDay)
                        context.AddFailure($"Hourly leave cannot exceed {leaveType.HoursPerDay} hours per day.");
                }
            });
    }
}