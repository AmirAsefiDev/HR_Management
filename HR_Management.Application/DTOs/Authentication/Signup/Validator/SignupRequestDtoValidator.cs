using FluentValidation;
using HR_Management.Application.DTOs.User.Validator;

namespace HR_Management.Application.DTOs.Authentication.Signup.Validator;

public class SignupRequestDtoValidator : AbstractValidator<SignupRequestDto>
{
    public SignupRequestDtoValidator()
    {
        RuleFor(s => s.FullName)
            .NotEmpty().WithMessage("Please enter your first and last name.");

        RuleFor(l => l.Mobile)
            .Must(PhoneNumberValidator.IsValidInternationalNumber)
            .When(x => !string.IsNullOrWhiteSpace(x.Mobile))
            .WithMessage(
                "Please enter a valid international phone number with country code (e.g., +1..., +44..., +98...).");

        RuleFor(l => l.Email)
            .NotEmpty().WithMessage("Pleas enter email.")
            .EmailAddress().WithMessage("Please enter your email correctly.");

        RuleFor(p => p.Password)
            .NotEmpty().WithMessage("Please enter password.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at lease one lowercase letter.")
            .Matches(@"\d").WithMessage("Password must contain at least one number.")
            .Matches(@"[!@#$%^&*(),.?""{}|<>]").WithMessage("Password must contain at least one special character.");
    }
}