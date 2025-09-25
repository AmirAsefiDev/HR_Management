using FluentValidation;
using HR_Management.Application.DTOs.User.EditProfile;

namespace HR_Management.Application.DTOs.User.Validator;

public class EditProfileValidator : AbstractValidator<EditProfileDto>
{
    public EditProfileValidator()
    {
        RuleFor(s => s.FullName)
            .NotEmpty().WithMessage("Please enter your first and last name.");

        RuleFor(l => l.Mobile)
            .Must(PhoneNumberValidator.IsValidInternationalNumber)
            .When(x => !string.IsNullOrWhiteSpace(x.Mobile))
            .WithMessage(
                "Please enter a valid international phone number with country code (e.g., +1..., +44..., +98...).");

        RuleFor(l => l.Email)
            .NotEmpty().WithMessage("Pleas enter your email.")
            .EmailAddress().WithMessage("Please enter your email correctly.");
    }
}