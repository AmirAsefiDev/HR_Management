using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.Authentication.RefreshToken;
using HR_Management.Application.Features.Authentication.Requests.Commands;
using HR_Management.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR_Management.Api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IMediator _mediator;
    private readonly IUserRepository _userRepo;
    private readonly IUserTokenRepository _userTokenRepo;

    public AuthController(
        IUserTokenRepository userTokenRepo,
        IUserRepository userRepo,
        IConfiguration configuration,
        IMediator mediator
    )
    {
        _userTokenRepo = userTokenRepo;
        _userRepo = userRepo;
        _configuration = configuration;
        _mediator = mediator;
    }

    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(ResultDto<RefreshTokenResponseDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultDto<RefreshTokenResponseDto>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<RefreshTokenResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto request)
    {
        var command = new RefreshTokenCommand
        {
            RefreshTokenRequestDto = request
        };
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    /*private CreateTokenResultDto CreateToken(CreateTokenDto user)
    {
        var key = _configuration["JwtConfig:Key"];
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokenExp = DateTime.Now.AddMinutes(int.Parse(_configuration["JwtConfig:AccessTokenMinutes"]));

        var claims = new List<Claim>
        {
            new("UserId", user.Id.ToString()),
            new("FullName", user.FullName ?? ""),
            new("Email", user.Email ?? "")
        };
        var token = new JwtSecurityToken(
            _configuration["JwtConfig:Issuer"],
            _configuration["JwtConfig:Audience"],
            expires: tokenExp,
            notBefore: DateTime.Now,
            signingCredentials: credentials,
            claims: claims
        );
        var tokenJwt = new JwtSecurityTokenHandler().WriteToken(token);
        var refreshToken = Guid.NewGuid();

        _userTokenRepo.SaveToken(new UserTokenDto
        {
            TokenExp = tokenExp,
            UserId = user.Id,
            HashedToken = SecurityHelper.GetSHA256Hash(tokenJwt),
            HashedRefreshToken = SecurityHelper.GetSHA256Hash(refreshToken.ToString()),
            RefreshTokenExp = DateTime.Now.AddDays(7)
        });

        return new CreateTokenResultDto
        {
            Token = tokenJwt,
            RefreshToken = refreshToken.ToString()
        };
    }*/
}