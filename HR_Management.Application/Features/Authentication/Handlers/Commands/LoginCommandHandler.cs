using HR_Management.Application.Contracts.Infrastructure.Authentication.JWT;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.Contracts.Persistence.Context;
using HR_Management.Application.DTOs.Authentication.Login;
using HR_Management.Application.DTOs.Authentication.Login.Validator;
using HR_Management.Application.DTOs.UserToken;
using HR_Management.Application.Features.Authentication.Requests.Commands;
using HR_Management.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HR_Management.Application.Features.Authentication.Handlers.Commands;

public class LoginCommandHandler : IRequestHandler<LoginCommand, ResultDto<LoginDto>>
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

    public async Task<ResultDto<LoginDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var validator = new LoginValidator();
        var validatorResult = await validator.ValidateAsync(request.LoginRequestDto, cancellationToken);
        if (!validatorResult.IsValid)
        {
            var errorMessage = validatorResult.Errors.First().ErrorMessage;
            return ResultDto<LoginDto>.Failure(errorMessage);
        }

        var formatedEmail = request.LoginRequestDto.Email.ToLower().Trim();
        var user = await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == formatedEmail, cancellationToken);

        if (user == null)
            return ResultDto<LoginDto>.Failure("No user registered with this email number.Please proceed to sign up.",
                409);


        var passwordHasher = new PasswordHasher();
        var verifyPasswordResult = passwordHasher.VerifyPassword(user.PasswordHash, request.LoginRequestDto.Password);
        if (!verifyPasswordResult)
            return ResultDto<LoginDto>.Failure("The email or password entered is incorrect.");

        user.LastLogin = DateTime.UtcNow;
        await _context.SaveChangesAsync(true, cancellationToken);

        var tokenProducer = await _jwtService.GenerateAsync(new UserTokenInput
        {
            UserId = user.Id,
            FullName = user.FullName,
            RoleName = user.Role.Name
        }, cancellationToken);

        await _userTokenRepo.SaveToken(new UserTokenDto
        {
            HashedToken = SecurityHelper.GetSHA256Hash(tokenProducer.AccessToken),
            TokenExp = tokenProducer.AccessTokenExpiresAtUtc,
            HashedRefreshToken = SecurityHelper.GetSHA256Hash(tokenProducer.RefreshToken),
            RefreshTokenExp = tokenProducer.RefreshTokenExpiresAtUtc,
            UserId = user.Id
        });

        return ResultDto<LoginDto>.Success(new LoginDto
        {
            AccessToken = tokenProducer.AccessToken,
            RefreshToken = tokenProducer.RefreshToken,
            RefreshTokenExp = tokenProducer.RefreshTokenExpiresAtUtc
        }, "Login was successful.");
    }
}