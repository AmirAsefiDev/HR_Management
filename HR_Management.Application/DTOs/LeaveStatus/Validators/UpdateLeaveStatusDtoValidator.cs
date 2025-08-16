using FluentValidation;

namespace HR_Management.Application.DTOs.LeaveStatus.Validators;

public class UpdateLeaveStatusDtoValidator : AbstractValidator<UpdateLeaveStatusDto>
{
    public UpdateLeaveStatusDtoValidator()
    {
        RuleFor(l => l.Id).NotNull().WithMessage("{PropertyName} is required.");
        Include(new ILeaveStatusDtoValidator());
    }
}