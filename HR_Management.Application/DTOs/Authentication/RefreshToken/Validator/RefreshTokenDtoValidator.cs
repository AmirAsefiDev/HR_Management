using FluentValidation;
using HR_Management.Application.Contracts.Persistence;

namespace HR_Management.Application.DTOs.Authentication.RefreshToken.Validator;

public class RefreshTokenDtoValidator : AbstractValidator<RefreshTokenRequestDto>
{
    private readonly IUserTokenRepository _userTokenRepo;

    public RefreshTokenDtoValidator(IUserTokenRepository userTokenRepo)
    {
        _userTokenRepo = userTokenRepo;

        RuleFor(r => r.RefreshToken)
            .NotEmpty().WithMessage("رفرش توکن معتبر نیست.");

        RuleFor(r => r.RefreshToken)
            .MustAsync(async (refToken, ct) =>
            {
                var userToken = await _userTokenRepo.FindByRefreshToken(refToken);
                return userToken != null;
            })
            .WithMessage("توکن یافت نشد یا معتبر نیست.");

        RuleFor(r => r.RefreshToken)
            .MustAsync(async (refToken, ct) =>
            {
                var userToken = await _userTokenRepo.FindByRefreshToken(refToken);
                if (userToken == null) return false;
                return userToken.RefreshTokenExp >= DateTime.Now;
            })
            .WithMessage("رفرش توکن منقضی شده است");
    }
}