using FluentValidation;

namespace HR_Management.Application.DTOs.Authentication.Login.Validator;

public class LoginValidator : AbstractValidator<LoginRequestDto>
{
    public LoginValidator()
    {
        RuleFor(l => l.Mobile)
            .NotEmpty().WithMessage("Please enter phone number.")
            .Matches(@"^(?:0|98|\+98|\+980|0098|098|00980)?(9\d{9})$")
            .WithMessage("Enter your phone number correctly.");

        RuleFor(l => l.Password)
            .NotEmpty().WithMessage("Please enter password.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long");
    }
}