using FluentValidation;

namespace HR_Management.Application.DTOs.Authentication.ForgetPassword.Validator;

public class ForgetPasswordValidator : AbstractValidator<ForgetPasswordRequestDto>
{
    public ForgetPasswordValidator()
    {
        RuleFor(l => l.Email)
            .NotEmpty().WithMessage("لطفا ایمیل را وارد کنید.")
            .EmailAddress().WithMessage("لطفا ایمیل خود را به درستی وارد نمایید.");
    }
}