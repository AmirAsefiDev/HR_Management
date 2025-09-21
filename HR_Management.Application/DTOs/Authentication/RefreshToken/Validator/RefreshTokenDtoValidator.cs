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
            .NotEmpty().WithMessage("Refresh token isn't valid.");

        RuleFor(r => r.RefreshToken)
            .MustAsync(async (refToken, ct) =>
            {
                var userToken = await _userTokenRepo.FindByRefreshToken(refToken);
                return userToken != null;
            })
            .WithMessage("Refresh token didn't find or isn't valid.");

        RuleFor(r => r.RefreshToken)
            .MustAsync(async (refToken, ct) =>
            {
                var userToken = await _userTokenRepo.FindByRefreshToken(refToken);
                if (userToken == null) return false;
                return userToken.RefreshTokenExp >= DateTime.Now;
            })
            .WithMessage("Refresh token has been deprecated.");
    }
}