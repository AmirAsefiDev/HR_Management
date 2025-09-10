using FluentValidation;
using HR_Management.Application.Contracts.Persistence;

namespace HR_Management.Application.DTOs.LeaveAllocation.Validators;

public class ILeaveAllocationDtoValidator : AbstractValidator<ILeaveAllocationDto>
{
    private readonly ILeaveTypeRepository _leaveTypeRepo;

    public ILeaveAllocationDtoValidator(ILeaveTypeRepository leaveTypeRepo)
    {
        _leaveTypeRepo = leaveTypeRepo;
        RuleFor(x => x.LeaveTypeId)
            .NotNull().WithMessage("Leave Type Id is required.")
            .MustAsync(async (id, token) => await _leaveTypeRepo.Exist(id))
            .WithMessage("{PropertyName} doesn't exist");

        RuleFor(x => x.Period).GreaterThan(0).WithMessage("Period must be greater than 0.");
        RuleFor(x => x.NumberOfDays).GreaterThan(0).WithMessage("Number of days must be greater than 0.");
    }
}