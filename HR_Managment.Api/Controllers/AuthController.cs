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
    [ProducesResponseType(typeof(ResultDto<SignupResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Signup([FromBody] SignupRequestDto request)
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
            RefreshToken = tokenPair.AccessToken
        });
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(ResultDto<LoginDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultDto<LoginDto>), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ResultDto<LoginResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
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
    [ProducesResponseType(typeof(ResultDto<RefreshTokenResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RefreshToken()
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
    public async Task<IActionResult> ForgetPassword(ForgetPasswordRequestDto request)
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
    public async Task<IActionResult> ResetPassword(ResetPasswordRequestDto request)
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
    public async Task<IActionResult> Logout()
    {
        var userId = User.Claims.First(u => u.Type == "UserId").Value;

        var command = new LogoutCommand { UserId = int.Parse(userId) };
        var result = _mediator.Send(command);

        return Ok(result);
    }
}