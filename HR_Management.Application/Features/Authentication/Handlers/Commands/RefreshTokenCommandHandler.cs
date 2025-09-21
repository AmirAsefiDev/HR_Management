using HR_Management.Application.Contracts.Infrastructure.Authentication.JWT;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.Authentication.RefreshToken;
using HR_Management.Application.DTOs.Authentication.RefreshToken.Validator;
using HR_Management.Application.DTOs.UserToken;
using HR_Management.Application.Features.Authentication.Requests.Commands;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.Authentication.Handlers.Commands;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, ResultDto<RefreshTokenDto>>
{
    private readonly IJWTTokenService _jwt;
    private readonly IUserTokenRepository _userTokenRepo;

    public RefreshTokenCommandHandler(IUserTokenRepository userTokenRepo, IJWTTokenService jwt)
    {
        _userTokenRepo = userTokenRepo;
        _jwt = jwt;
    }

    public async Task<ResultDto<RefreshTokenDto>> Handle(RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var validator = new RefreshTokenDtoValidator(_userTokenRepo);
        var validationResult = await validator.ValidateAsync(request.RefreshTokenRequestDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            var errorMessage = validationResult.Errors.First().ErrorMessage;
            if (errorMessage.Contains("deprecated") || errorMessage.Contains("didn't find"))
                return ResultDto<RefreshTokenDto>.Failure(errorMessage, 401);

            return ResultDto<RefreshTokenDto>.Failure(errorMessage);
        }

        var userToken = await _userTokenRepo.FindByRefreshToken(request.RefreshTokenRequestDto.RefreshToken);
        var tokenProducer = await _jwt.GenerateAsync(new UserTokenInput
        {
            UserId = userToken.UserId,
            FullName = userToken.User.FullName,
            RoleName = userToken.User.Role.Name
        }, cancellationToken);

        await _userTokenRepo.SaveToken(new UserTokenDto
        {
            UserId = userToken.UserId,
            HashedToken = SecurityHelper.GetSHA256Hash(tokenProducer.AccessToken),
            TokenExp = tokenProducer.AccessTokenExpiresAtUtc,
            HashedRefreshToken = tokenProducer.RefreshToken,
            RefreshTokenExp = tokenProducer.RefreshTokenExpiresAtUtc
        });

        return ResultDto<RefreshTokenDto>.Success(
            new RefreshTokenDto
            {
                AccessToken = tokenProducer.AccessToken,
                RefreshToken = tokenProducer.RefreshToken,
                RefreshTokenExp = tokenProducer.RefreshTokenExpiresAtUtc
            }, "A new token has been successfully generated.");
    }
}