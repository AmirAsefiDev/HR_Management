using HR_Management.Application.Contracts.Persistence.Context;
using HR_Management.Application.DTOs.Authentication.ResetPassword.Validator;
using HR_Management.Application.Features.Authentication.Requests.Commands;
using HR_Management.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HR_Management.Application.Features.Authentication.Handlers.Commands;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ResultDto>
{
    private readonly ILeaveManagementDbContext _context;

    public ResetPasswordCommandHandler(ILeaveManagementDbContext context)
    {
        _context = context;
    }

    public async Task<ResultDto> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var validator = new ResetPasswordValidator();
        var validatorResult = await validator.ValidateAsync(request.ResetPasswordRequestDto, cancellationToken);
        if (!validatorResult.IsValid)
            return ResultDto.Failure(validatorResult.Errors.First().ErrorMessage);

        var resetToken = await _context.PasswordResetTokens
            .FirstOrDefaultAsync(t => t.Token == request.ResetPasswordRequestDto.Token, cancellationToken);

        if (resetToken == null || resetToken.IsUsed || resetToken.ExpireAt < DateTime.UtcNow)
            return ResultDto.Failure("Token isn't valid or is deprecated.");

        var user = await _context.Users.FindAsync(resetToken.UserId, cancellationToken);
        if (user == null)
            return ResultDto.Failure("No user found.");

        var passwordHasher = new PasswordHasher();

        user.PasswordHash = passwordHasher.HashPassword(request.ResetPasswordRequestDto.NewPassword);
        resetToken.IsUsed = true;

        await _context.SaveChangesAsync(true, cancellationToken);

        return ResultDto.Success("Password has been successfully changed.");
    }
}