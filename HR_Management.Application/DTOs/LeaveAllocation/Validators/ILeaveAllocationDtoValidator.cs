using FluentValidation;
using HR_Management.Application.Persistence.Contracts;

namespace HR_Management.Application.DTOs.LeaveAllocation.Validators;

public class ILeaveAllocationDtoValidator : AbstractValidator<ILeaveAllocationDto>
{
    private readonly ILeaveTypeRepository _leaveTypeRepo;

    public ILeaveAllocationDtoValidator(ILeaveTypeRepository leaveTypeRepo)
    {
        _leaveTypeRepo = leaveTypeRepo;
        RuleFor(x => x.LeaveTypeId)
            .NotNull().WithMessage("Leave Type Id is required.")
            .MustAsync(async (id, token) => !await _leaveTypeRepo.Exist(id))
            .WithMessage("{PropertyName} doesn't exist");

        RuleFor(x => x.Priod).LessThanOrEqualTo(0).WithMessage("Priod must be greater than 0.");
        RuleFor(x => x.NumberOfDays).LessThanOrEqualTo(0).WithMessage("Number of days must be greater than 0.");
    }
}