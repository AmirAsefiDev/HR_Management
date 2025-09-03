using FluentValidation;

namespace HR_Management.Application.DTOs.Authentication.Login.Validator;

public class LoginValidator : AbstractValidator<LoginRequestDto>
{
    public LoginValidator()
    {
        RuleFor(l => l.Mobile)
            .NotEmpty().WithMessage("لطفا شماره تلفن را وارد کنید.")
            .Matches(@"^(?:0|98|\+98|\+980|0098|098|00980)?(9\d{9})$")
            .WithMessage("شماره موبایل خود را به درستی وارد نمایید");

        RuleFor(l => l.Password)
            .NotEmpty().WithMessage("لطفا رمز عبور را وارد کنید.")
            .MinimumLength(6).WithMessage("رمز عبور باید حداقل 6 کاراکتر باشد");
    }
}