using FluentValidation;

namespace HR_Management.Application.DTOs.LeaveType.Validators;

public class UpdateLeaveTypeDtoValidator : AbstractValidator<LeaveTypeDto>
{
    public UpdateLeaveTypeDtoValidator()
    {
        Include(new ILeaveTypeDtoValidator());

        RuleFor(l => l.Id).NotNull().WithMessage("{PropertyName} is required.");

        RuleFor(l => l.Id).GreaterThan(0).WithMessage("Please Enter LeaveTypeId Correctly");
    }
}