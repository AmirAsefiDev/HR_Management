using HR_Management.Application.Contracts.Infrastructure.Authentication.JWT;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.Authentication.Login;
using HR_Management.Application.DTOs.Authentication.Login.Validator;
using HR_Management.Application.DTOs.UserToken;
using HR_Management.Application.Features.Authentication.Requests.Commands;
using HR_Management.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HR_Management.Application.Features.Authentication.Handlers.Commands;

public class LoginCommandHandler : IRequestHandler<LoginCommand, ResultDto<LoginResponseDto>>
{
    private readonly ILeaveManagementDbContext _context;
    private readonly IJWTTokenService _jwtService;
    private readonly IUserTokenRepository _userTokenRepo;

    public LoginCommandHandler(
        ILeaveManagementDbContext context,
        IJWTTokenService jwtService,
        IUserTokenRepository userTokenRepo
    )
    {
        _context = context;
        _jwtService = jwtService;
        _userTokenRepo = userTokenRepo;
    }

    public async Task<ResultDto<LoginResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var validator = new LoginValidator();
        var validatorResult = await validator.ValidateAsync(request.LoginRequestDto, cancellationToken);
        if (!validatorResult.IsValid)
        {
            var errorMessage = validatorResult.Errors.First().ErrorMessage;
            return ResultDto<LoginResponseDto>.Failure(errorMessage);
        }

        var formatedMobile = Convertors.ConvertMobileToRawFormat(request.LoginRequestDto.Mobile);
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Mobile == formatedMobile, cancellationToken);

        if (user == null)
            return ResultDto<LoginResponseDto>.Failure(
                "کاربری با این شماره تلفن ثبت نام نکرده،لطفا جهت ثبت نام اقدام نمایید.", 409);

        var passwordHasher = new PasswordHasher();
        var verifyPasswordResult = passwordHasher.VerifyPassword(user.PasswordHash, request.LoginRequestDto.Password);
        if (!verifyPasswordResult)
            return ResultDto<LoginResponseDto>.Failure("شماره تلفن یا رمز عبور وارد شده نادرست است");

        var tokenProducer = await _jwtService.GenerateAsync(new UserTokenInput
        {
            UserId = user.Id,
            Email = user.Email,
            FullName = user.FullName,
            Role = user.Role
        }, cancellationToken);
        await _userTokenRepo.SaveToken(new UserTokenDto
        {
            HashedToken = SecurityHelper.GetSHA256Hash(tokenProducer.AccessToken),
            TokenExp = tokenProducer.AccessTokenExpiresAtUtc,
            HashedRefreshToken = SecurityHelper.GetSHA256Hash(tokenProducer.RefreshToken),
            RefreshTokenExp = tokenProducer.RefreshTokenExpiresAtUtc,
            UserId = user.Id
        });

        return ResultDto<LoginResponseDto>.Success(new LoginResponseDto
        {
            AccessToken = tokenProducer.AccessToken,
            RefreshToken = tokenProducer.RefreshToken
        }, "ورود با موفقیت انجام شد.");
    }
}