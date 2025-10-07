using System.Security.Claims;
using HR_Management.Application.Contracts.Infrastructure.Authentication.JWT;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.Authentication.ForgetPassword;
using HR_Management.Application.DTOs.Authentication.Login;
using HR_Management.Application.DTOs.Authentication.RefreshToken;
using HR_Management.Application.DTOs.Authentication.ResetPassword;
using HR_Management.Application.DTOs.Authentication.Signup;
using HR_Management.Application.Features.Authentication.Requests.Commands;
using HR_Management.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR_Management.Api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IJWTTokenService _jwtTokenService;
    private readonly IMediator _mediator;
    private readonly IUserRepository _userRepo;

    public AuthController(
        IMediator mediator,
        IJWTTokenService jwtTokenService
    )
    {
        _mediator = mediator;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("signup")]
    [ProducesResponseType(typeof(ResultDto<SignupDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultDto<SignupDto>), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(SignupResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Signup([FromBody] SignupRequestDto request)
    {
        var command = new SignupCommand
        {
            SignupRequestDto = request
        };
        var result = await _mediator.Send(command);
        if (!result.IsSuccess)
            return StatusCode(result.StatusCode, result);

        var tokenPair = result.Data;
        _jwtTokenService.SerRefreshTokenCookie(HttpContext, tokenPair.RefreshToken, tokenPair.RefreshTokenExp);

        return Ok(new SignupResponseDto
        {
            AccessToken = tokenPair.AccessToken
        });
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(ResultDto<LoginDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultDto<LoginDto>), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Login([FromBody] LoginRequestDto request)
    {
        var command = new LoginCommand
        {
            LoginRequestDto = request
        };
        var result = await _mediator.Send(command);
        if (!result.IsSuccess)
            return StatusCode(result.StatusCode, result);
        var tokenPair = result.Data;
        _jwtTokenService.SerRefreshTokenCookie(HttpContext, tokenPair.RefreshToken, tokenPair.RefreshTokenExp);

        return Ok(new LoginResponseDto
        {
            AccessToken = tokenPair.AccessToken
        });
    }

    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(ResultDto<RefreshTokenDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultDto<RefreshTokenDto>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(RefreshTokenResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        if (string.IsNullOrEmpty(refreshToken))
            return Unauthorized(ResultDto<RefreshTokenDto>.Failure("رفرش توکن یافت نشد"));

        var command = new RefreshTokenCommand
        {
            RefreshTokenRequestDto = new RefreshTokenRequestDto { RefreshToken = refreshToken }
        };
        var result = await _mediator.Send(command);
        if (!result.IsSuccess)
            return StatusCode(result.StatusCode, result);
        var tokenPair = result.Data;

        _jwtTokenService.SerRefreshTokenCookie(HttpContext, tokenPair.RefreshToken, tokenPair.RefreshTokenExp);

        return Ok(new RefreshTokenResponseDto
        {
            AccessToken = tokenPair.AccessToken
        });
    }

    [HttpPost("forget-password")]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ForgetPassword(ForgetPasswordRequestDto request)
    {
        var command = new ForgetPasswordCommand
        {
            ForgetPasswordRequestDto = request
        };
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("reset-password")]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ResetPassword(ResetPasswordRequestDto request)
    {
        var command = new ResetPasswordCommand
        {
            ResetPasswordRequestDto = request
        };
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("logout")]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status200OK)]
    [Authorize]
    public async Task<ActionResult> Logout()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        if (!int.TryParse(userIdClaim, out var userId))
            return Unauthorized();
        var command = new LogoutCommand { UserId = userId };
        var result = await _mediator.Send(command);

        return Ok(result);
    }
}