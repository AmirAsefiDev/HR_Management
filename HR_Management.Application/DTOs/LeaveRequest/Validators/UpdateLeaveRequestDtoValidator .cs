using FluentValidation;
using HR_Management.Application.Persistence.Contracts;

namespace HR_Management.Application.DTOs.LeaveRequest.Validators;

public class UpdateLeaveRequestDtoValidator : AbstractValidator<UpdateLeaveRequestDto>
{
    private readonly ILeaveTypeRepository _leaveTypeRepo;

    public UpdateLeaveRequestDtoValidator(ILeaveTypeRepository leaveTypeRepo)
    {
        _leaveTypeRepo = leaveTypeRepo;
        Include(new ILeaveRequestDtoValidator(_leaveTypeRepo));

        RuleFor(l => l.Id).NotNull().WithMessage("{PropertyName} is required.");
    }
}