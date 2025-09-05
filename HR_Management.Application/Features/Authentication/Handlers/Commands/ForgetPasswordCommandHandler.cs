using System.Security.Cryptography;
using ERP.Application.Interfaces.Email;
using HR_Management.Application.Contracts.Persistence.Context;
using HR_Management.Application.DTOs.Authentication.ForgetPassword.Validator;
using HR_Management.Application.Features.Authentication.Requests.Commands;
using HR_Management.Common;
using HR_Management.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HR_Management.Application.Features.Authentication.Handlers.Commands;

public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, ResultDto>
{
    private readonly ILeaveManagementDbContext _context;
    private readonly IEmailService _emailService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<ForgetPasswordCommandHandler> _logger;

    public ForgetPasswordCommandHandler
    (
        ILeaveManagementDbContext context,
        IEmailService emailService,
        IHttpContextAccessor httpContextAccessor,
        ILogger<ForgetPasswordCommandHandler> logger
    )
    {
        _context = context;
        _emailService = emailService;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public async Task<ResultDto> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
    {
        var validator = new ForgetPasswordValidator();
        var validatorResult = await validator.ValidateAsync(request.ForgetPasswordRequestDto, cancellationToken);
        if (!validatorResult.IsValid)
            return ResultDto.Failure(validatorResult.Errors.First().ErrorMessage);

        var formatedEmail = request.ForgetPasswordRequestDto.Email.Trim().ToLower();
        var isUserExists = await _context.Users.FirstOrDefaultAsync(u => u.Email == formatedEmail, cancellationToken);
        if (isUserExists == null)
            return ResultDto.Failure("کاربری با این ایمیل نشده، لطفا ثبت نام نمایید.");
        var secureToken = GenerateSecureToken();
        var passwordResetToken = new PasswordResetToken
        {
            UserId = isUserExists.Id,
            ExpireAt = DateTime.UtcNow.AddMinutes(15),
            Token = secureToken
        };

        await _context.PasswordResetTokens.AddAsync(passwordResetToken, cancellationToken);
        await _context.SaveChangesAsync(true, cancellationToken);


        var httpRequest = _httpContextAccessor.HttpContext?.Request;
        var domain = $"{httpRequest?.Scheme}://{httpRequest?.Host}";
        var resetLink = $"{domain}/reset-password?token={secureToken}";
        var emailDTO = new EmailDto
        {
            Destination = formatedEmail,
            Title = "Reset Password",
            MessageBody = "<!DOCTYPE html>\r\n" +
                          "<html>\r\n" +
                          "<head>\r\n " +
                          " <meta charset=\"utf-8\">\r\n  " +
                          "<title>Password Reset</title>\r\n" +
                          "</head>\r\n" +
                          "<body style=\"font-family: Arial, sans-serif; line-height: 1.6; color: #333;\">\r\n" +
                          "  <h2>Reset Your Password</h2>\r\n" +
                          "  <p>Hello,</p>\r\n" +
                          "  <p>You requested to reset your password. Please click the button below to continue:</p>\r\n " +
                          " \r\n  <p style=\"text-align:center;\">\r\n  " +
                          $"           <a href=\"{resetLink}\" \r\n       style=\"display:inline-block; padding:12px 20px; background-color:#007BFF; color:#fff; text-decoration:none; border-radius:5px;\">\r\n " +
                          "      Reset Password\r\n    </a>\r\n " +
                          " </p>\r\n  \r\n" +
                          "  <p>If the button doesn’t work, copy and paste this link into your browser:</p>\r\n " +
                          $" <p><a href=\"{resetLink}\">{resetLink}</a></p>\r\n  \r\n  " +
                          "<hr />\r\n " +
                          " <p style=\"font-size:12px; color:#777;\">\r\n    " +
                          "This link will expire in 15 minutes. If you didn’t request a password reset, please ignore this email.\r\n " +
                          " </p>\r\n" +
                          "</body>\r\n" +
                          "</html>\r\n"
        };
        try
        {
            await _emailService.SendEmail(emailDTO);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "an error happened while ending email to forget password");
            throw new Exception(e.Message);
        }

        return ResultDto.Success("لینک تغییر رمز عبور با موفقیت به ایمیل شما ارسال شد.");
    }

    private string GenerateSecureToken()
    {
        var bytes = RandomNumberGenerator.GetBytes(32);
        return Convert.ToBase64String(bytes)
            .Replace("+", "-")
            .Replace("/", "_")
            .TrimEnd('=');
    }
}