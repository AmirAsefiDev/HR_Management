using FluentValidation;

namespace HR_Management.Application.DTOs.Authentication.ForgetPassword.Validator;

public class ForgetPasswordValidator : AbstractValidator<ForgetPasswordRequestDto>
{
    public ForgetPasswordValidator()
    {
        RuleFor(l => l.Email)
            .NotEmpty().WithMessage("Please enter your email. ")
            .EmailAddress().WithMessage("Please enter your email correctly.");
    }
}