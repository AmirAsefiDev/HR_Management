using FluentValidation;

namespace HR_Management.Application.DTOs.LeaveStatus.Validators;

public class UpdateLeaveStatusDtoValidator : AbstractValidator<UpdateLeaveStatusDto>
{
    public UpdateLeaveStatusDtoValidator()
    {
        RuleFor(l => l.Id)
            .NotNull().WithMessage("{PropertyName} is required.")
            .GreaterThan(0).WithMessage("Please Enter Id Correctly");
        Include(new ILeaveStatusDtoValidator());
    }
}