using System.Security.Claims;
using HR_Management.Application.DTOs.User;
using HR_Management.Application.DTOs.User.EditProfile;
using HR_Management.Application.Features.User.Requests.Commands;
using HR_Management.Application.Features.User.Requests.Queries;
using HR_Management.Common;
using HR_Management.Common.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR_Management.Api.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Retrieves the details of authenticated user for profile page.
    /// </summary>
    /// <returns>User Profile details.</returns>
    /// <remarks>
    ///     Sample Request:
    ///     Get api/users/me
    /// </remarks>
    [HttpGet("me")]
    [Authorize(Policy = Permissions.UserRead)]
    [ProducesResponseType(typeof(GetUserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetUserInfo()
    {
        //receive userId by Token 
        var claimUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        if (claimUserId is null || !int.TryParse(claimUserId, out var userId))
            return Unauthorized();

        var result = await _mediator.Send(new GetUserDetailRequest
        {
            UserId = userId
        });
        return Ok(result);
    }

    /// <summary>
    ///     Updates the authenticated user page information.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <remarks>
    ///     Sample request:
    ///     PUT api/users
    ///     {
    ///     "fullName":"Amir Asefi",
    ///     "email":"amirasefi.info@gmail.com",
    ///     "mobile":"+989123456789"
    ///     }
    /// </remarks>
    [HttpPut]
    [Authorize(Policy = Permissions.UserEditProfile)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> EditProfile([FromBody] EditProfileRequestDto request)
    {
        //receive userId by JWT Token 
        var claimUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        if (claimUserId is null || !int.TryParse(claimUserId, out var userId))
            return Unauthorized();

        var result = await _mediator.Send(new EditProfileCommand
        {
            EditProfileDto = new EditProfileDto
            {
                Id = userId,
                Email = request.Email,
                FullName = request.FullName,
                Mobile = request.Mobile
            }
        });
        return Ok(result);
    }
}