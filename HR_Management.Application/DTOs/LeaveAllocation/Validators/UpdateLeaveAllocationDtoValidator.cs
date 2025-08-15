using FluentValidation;
using HR_Management.Application.Persistence.Contracts;

namespace HR_Management.Application.DTOs.LeaveAllocation.Validators;

public class UpdateLeaveAllocationDtoValidator : AbstractValidator<UpdateLeaveAllocationDto>
{
    private readonly ILeaveTypeRepository _leaveTypeRepo;

    public UpdateLeaveAllocationDtoValidator(ILeaveTypeRepository leaveTypeRepo)
    {
        _leaveTypeRepo = leaveTypeRepo;
        Include(new ILeaveAllocationDtoValidator(_leaveTypeRepo));
        RuleFor(l => l.Id).NotNull().WithMessage("{PropertyName} is required.");
    }
}