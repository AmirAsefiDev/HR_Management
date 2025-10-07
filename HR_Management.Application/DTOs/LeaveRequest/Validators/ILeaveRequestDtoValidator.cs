using FluentValidation;
using HR_Management.Application.Contracts.Persistence;

namespace HR_Management.Application.DTOs.LeaveRequest.Validators;

public class ILeaveRequestDtoValidator : AbstractValidator<ILeaveRequestDto>
{
    private readonly ILeaveStatusRepository _leaveStatusRepo;
    private readonly ILeaveTypeRepository _leaveTypeRepo;

    public ILeaveRequestDtoValidator(ILeaveTypeRepository leaveTypeRepo,
        ILeaveStatusRepository leaveStatusRepo)
    {
        _leaveTypeRepo = leaveTypeRepo;
        _leaveStatusRepo = leaveStatusRepo;

        RuleFor(l => l.StartDate)
            .LessThan(l => l.EndDate)
            .WithMessage("{PropertyName} must be before {ComparisonValue}");

        RuleFor(l => l.EndDate)
            .GreaterThan(l => l.StartDate)
            .WithMessage("{PropertyName} must be after {ComparisonValue}");

        RuleFor(l => l.LeaveTypeId)
            .GreaterThan(0)
            //optimized version
            .MustAsync(async (id, token) => await _leaveTypeRepo.ExistAsync(id))
            //simple version
            //.MustAsync(async (id, token) =>
            //{
            //    var leaveTypeExist = await _leaveRequestRepo.Exist(id);
            //    return leaveTypeExist;
            //})
            .WithMessage("{PropertyName} doesn't exist");

        RuleFor(l => l.LeaveStatusId)
            .GreaterThan(0)
            //optimized version
            .MustAsync(async (id, token) => await _leaveStatusRepo.ExistAsync(id))
            .WithMessage("{PropertyName} doesn't exist");
    }
}