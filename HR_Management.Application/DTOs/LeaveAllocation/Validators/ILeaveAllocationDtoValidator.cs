using FluentValidation;
using HR_Management.Application.Contracts.Persistence;

namespace HR_Management.Application.DTOs.LeaveAllocation.Validators;

public class ILeaveAllocationDtoValidator : AbstractValidator<ILeaveAllocationDto>
{
    private readonly ILeaveTypeRepository _leaveTypeRepo;

    public ILeaveAllocationDtoValidator(ILeaveTypeRepository leaveTypeRepo)
    {
        _leaveTypeRepo = leaveTypeRepo;

        RuleFor(la => la.LeaveTypeId)
            .NotNull().WithMessage("Leave Type Id is required.")
            .MustAsync(async (id, token) => await _leaveTypeRepo.ExistAsync(id))
            .WithMessage("{PropertyName} doesn't exist");

        RuleFor(la => la.UserId)
            .NotNull().WithMessage("{PropertyName} is required.")
            .GreaterThanOrEqualTo(0).WithMessage("Please enter {PropertyName} correctly.");
    }
}