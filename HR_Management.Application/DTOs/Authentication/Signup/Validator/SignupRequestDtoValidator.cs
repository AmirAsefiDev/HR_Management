using FluentValidation;

namespace HR_Management.Application.DTOs.Authentication.Signup.Validator;

public class SignupRequestDtoValidator : AbstractValidator<SignupRequestDto>
{
    public SignupRequestDtoValidator()
    {
        RuleFor(s => s.FullName)
            .NotEmpty().WithMessage("لطفا نام و فامیلی را وارد کنید.");

        RuleFor(l => l.Mobile)
            .NotEmpty().WithMessage("لطفا شماره تلفن را وارد کنید.")
            .Matches(@"^(?:0|98|\+98|\+980|0098|098|00980)?(9\d{9})$")
            .WithMessage("شماره موبایل خود را به درستی وارد نمایید");

        RuleFor(l => l.Email)
            .NotEmpty().WithMessage("لطفا ایمیل را وارد کنید.")
            .EmailAddress().WithMessage("لطفا ایمیل خود را به درستی وارد نمایید.");

        RuleFor(l => l.Password)
            .NotEmpty().WithMessage("لطفا رمز عبور را وارد کنید.")
            .MinimumLength(6).WithMessage("رمز عبور باید حداقل 6 کاراکتر باشد")
            .Matches(@"[A-Z]").WithMessage("رمز عبور باید حداقل یک حرف بزرگ داشته باشد.")
            .Matches(@"[a-z]").WithMessage("رمز عبور باید حداقل یک حرف کوچک داشته باشد.")
            .Matches(@"\d").WithMessage("رمز عبور باید حداقل یک عدد داشته باشد.")
            .Matches(@"[!@#$%^&*(),.?""{}|<>]").WithMessage("رمز عبور باید حداقل یک کاراکتر خاص داشته باشد.");
    }
}