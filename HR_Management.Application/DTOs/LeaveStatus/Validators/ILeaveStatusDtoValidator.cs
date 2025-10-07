using FluentValidation;

namespace HR_Management.Application.DTOs.LeaveStatus.Validators;

public class ILeaveStatusDtoValidator : AbstractValidator<ILeaveStatusDto>
{
    public ILeaveStatusDtoValidator()
    {
        RuleFor(l => l.Name).NotEmpty().WithMessage("Please Enter {PropertyName}.");
        RuleFor(l => l.Name).Length(100).WithMessage("Please Enter {PropertyName} shorter.");

        RuleFor(l => l.Description)
            .Empty().Length(300).WithMessage("Please Enter {PropertyName} shorter.");
    }
}