using HR_Management.Application.Contracts.Infrastructure.Authentication.JWT;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.Contracts.Persistence.Context;
using HR_Management.Application.DTOs.Authentication.Signup;
using HR_Management.Application.DTOs.Authentication.Signup.Validator;
using HR_Management.Application.DTOs.UserToken;
using HR_Management.Application.Features.Authentication.Requests.Commands;
using HR_Management.Common;
using HR_Management.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HR_Management.Application.Features.Authentication.Handlers.Commands;

public class SignupCommandHandler : IRequestHandler<SignupCommand, ResultDto<SignupDto>>
{
    private readonly ILeaveManagementDbContext _context;
    private readonly IJWTTokenService _jwtService;
    private readonly ILeaveAllocationRepository _leaveAllocationRepo;
    private readonly ILeaveTypeRepository _leaveTypeRepo;
    private readonly IUserTokenRepository _userTokenRepo;

    public SignupCommandHandler(
        ILeaveManagementDbContext context,
        IJWTTokenService jwtService,
        IUserTokenRepository userTokenRepo,
        ILeaveTypeRepository leaveTypeRepo,
        ILeaveAllocationRepository leaveAllocationRepo
    )
    {
        _context = context;
        _jwtService = jwtService;
        _userTokenRepo = userTokenRepo;
        _leaveTypeRepo = leaveTypeRepo;
        _leaveAllocationRepo = leaveAllocationRepo;
    }

    public async Task<ResultDto<SignupDto>> Handle(SignupCommand request, CancellationToken cancellationToken)
    {
        var validator = new SignupRequestDtoValidator();
        var validatorResult = await validator.ValidateAsync(request.SignupRequestDto, cancellationToken);

        if (!validatorResult.IsValid)
        {
            var errorMessage = validatorResult.Errors.First().ErrorMessage;
            return ResultDto<SignupDto>.Failure(errorMessage);
        }

        string? formatedMobile = null;
        var countryCode = 0;
        if (!string.IsNullOrWhiteSpace(request.SignupRequestDto.Mobile))
        {
            formatedMobile = Convertors.ToRawNationalNumber(request.SignupRequestDto.Mobile);
            countryCode = Convertors.GetCountryCode(request.SignupRequestDto.Mobile);
        }

        //prevent user to record repeatedly Email & Mobile
        var isUserExists = await _context.Users.FirstOrDefaultAsync(u => u.Mobile == formatedMobile, cancellationToken);
        if (isUserExists != null)
            return ResultDto<SignupDto>.Failure("You have already registered.Please go to the 'Login' from to sign in.",
                409);
        var isEmailExists =
            await _context.Users.FirstOrDefaultAsync(u => u.Email == request.SignupRequestDto.Email, cancellationToken);
        if (isEmailExists != null)
            return ResultDto<SignupDto>.Failure("You have already registered.Please go to the 'Login' from to sign in.",
                409);

        var passwordHasher = new PasswordHasher();
        var hashedPassword = passwordHasher.HashPassword(request.SignupRequestDto.Password);

        var newUser = new Domain.User
        {
            CreatedAt = DateTime.UtcNow,
            Email = request.SignupRequestDto.Email.Trim().ToLower(),
            Mobile = formatedMobile,
            CountryCode = countryCode,
            FullName = request.SignupRequestDto.FullName.Trim(),
            PasswordHash = hashedPassword,
            LastLogin = DateTime.UtcNow,
            Picture = "/images/user/default_profile.jpg"
        };

        await _context.Users.AddAsync(newUser, cancellationToken);
        await _context.SaveChangesAsync(true, cancellationToken);

        var leaveTypes = await _leaveTypeRepo.GetAllAsync();
        foreach (var leaveType in leaveTypes)
            await _leaveAllocationRepo.AddAsync(new LeaveAllocation
            {
                LeaveTypeId = leaveType.Id,
                TotalDays = leaveType.DefaultDay,
                UserId = newUser.Id,
                Period = DateTime.UtcNow.Year
            });

        var role = await _context.Roles.FindAsync(newUser.Id, cancellationToken);

        var tokenProducer = await _jwtService.GenerateAsync(new UserTokenInput
        {
            FullName = newUser.FullName,
            RoleName = role.Name,
            UserId = newUser.Id
        }, cancellationToken);

        await _userTokenRepo.SaveTokenAsync(new UserTokenDto
        {
            HashedToken = SecurityHelper.GetSHA256Hash(tokenProducer.AccessToken),
            TokenExp = tokenProducer.AccessTokenExpiresAtUtc,
            HashedRefreshToken = SecurityHelper.GetSHA256Hash(tokenProducer.RefreshToken),
            RefreshTokenExp = tokenProducer.RefreshTokenExpiresAtUtc,
            UserId = newUser.Id
        });

        return ResultDto<SignupDto>.Success(new SignupDto
        {
            AccessToken = tokenProducer.AccessToken,
            RefreshToken = tokenProducer.RefreshToken,
            RefreshTokenExp = tokenProducer.RefreshTokenExpiresAtUtc
        }, "You have successfully registered.");
    }
}