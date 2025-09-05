using FluentValidation;

namespace HR_Management.Application.DTOs.Authentication.ResetPassword.Validator;

public class ResetPasswordValidator : AbstractValidator<ResetPasswordRequestDto>
{
    public ResetPasswordValidator()
    {
        RuleFor(p => p.NewPassword)
            .NotEmpty().WithMessage("لطفا رمز عبور را وارد کنید.")
            .MinimumLength(6).WithMessage("رمز عبور باید حداقل 6 کاراکتر باشد")
            .Matches(@"[A-Z]").WithMessage("رمز عبور باید حداقل یک حرف بزرگ داشته باشد.")
            .Matches(@"[a-z]").WithMessage("رمز عبور باید حداقل یک حرف کوچک داشته باشد.")
            .Matches(@"\d").WithMessage("رمز عبور باید حداقل یک عدد داشته باشد.")
            .Matches(@"[!@#$%^&*(),.?""{}|<>]").WithMessage("رمز عبور باید حداقل یک کاراکتر خاص داشته باشد.");

        RuleFor(p => p.Token).NotEmpty().WithMessage("لطفا توکن را وارد کنید.");
    }
}