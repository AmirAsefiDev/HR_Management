using FluentValidation;
using HR_Management.Application.Contracts.Persistence;

namespace HR_Management.Application.DTOs.LeaveRequest.Validators;

public class UpdateLeaveRequestDtoValidator : AbstractValidator<UpdateLeaveRequestDto>
{
    private readonly ILeaveStatusRepository _leaveStatusRepo;
    private readonly ILeaveTypeRepository _leaveTypeRepo;

    public UpdateLeaveRequestDtoValidator(ILeaveTypeRepository leaveTypeRepo, ILeaveStatusRepository leaveStatusRepo)
    {
        _leaveTypeRepo = leaveTypeRepo;
        _leaveStatusRepo = leaveStatusRepo;
        Include(new ILeaveRequestDtoValidator(_leaveTypeRepo, _leaveStatusRepo));

        RuleFor(l => l.Id)
            .NotNull().WithMessage("{PropertyName} is required.")
            .GreaterThan(0).WithMessage("Please Enter LeaveRequestId Correctly");
    }
}