using FluentValidation;

namespace HR_Management.Application.DTOs.Authentication.Login.Validator;

public class LoginValidator : AbstractValidator<LoginRequestDto>
{
    public LoginValidator()
    {
        RuleFor(l => l.Email)
            .NotEmpty().WithMessage("Pleas enter email.")
            .EmailAddress().WithMessage("Please enter your email correctly.");

        RuleFor(l => l.Password)
            .NotEmpty().WithMessage("Please enter password.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long");
    }
}