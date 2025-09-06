using FluentValidation;

namespace HR_Management.Application.DTOs.LeaveType.Validators;

public class ILeaveTypeDtoValidator : AbstractValidator<ILeaveTypeDto>
{
    public ILeaveTypeDtoValidator()
    {
        RuleFor(l => l.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull().WithMessage("{PropertyName} must not exceed 50");

        RuleFor(p => p.DefaultDay)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} Must be at least 1")
            .LessThanOrEqualTo(100).WithMessage("{PropertyName} Must be at last 100");
    }
}