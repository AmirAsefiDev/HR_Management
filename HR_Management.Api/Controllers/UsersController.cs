using System.Security.Claims;
using HR_Management.Application.DTOs.User;
using HR_Management.Application.DTOs.User.EditProfile;
using HR_Management.Application.DTOs.User.UpdateUserPicture;
using HR_Management.Application.Features.User.Requests.Commands;
using HR_Management.Application.Features.User.Requests.Queries;
using HR_Management.Common;
using HR_Management.Common.Pagination;
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
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status400BadRequest)]
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
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    ///     Uploads or updates the profile picture of the authenticated user.
    /// </summary>
    /// <param name="request">
    ///     The request containing the profile picture file to be uploaded (sent as form-data).
    /// </param>
    /// <remarks>
    ///     Sample request:
    ///     PATCH api/users/upload-profile-picture
    ///     Content-Type: multipart/form-data
    ///     Form-data:
    ///     {
    ///     "picture": [binary file e.g. profile.jpg]
    ///     }
    /// </remarks>
    [HttpPatch("upload-profile-picture")]
    [Authorize(Policy = Permissions.UserEditProfile)]
    [ProducesResponseType(typeof(ResultDto<UpdateUserPictureResponseDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultDto<UpdateUserPictureResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateUserPicture([FromForm] UpdateUserPictureRequestDto request)
    {
        //receive userId by JWT Token 
        var claimUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        if (claimUserId is null || !int.TryParse(claimUserId, out var userId))
            return Unauthorized();

        var result = await _mediator.Send(new UpdateUserPictureCommand
        {
            UpdateUserPictureDto = new UpdateUserPictureDto
            {
                Picture = request.Picture,
                UserId = userId
            }
        });

        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    ///     Deletes the profile picture of the authenticated user.
    /// </summary>
    /// <param name="picture">
    ///     The file name or path of the picture to delete (provided as a query parameter).
    /// </param>
    /// <returns></returns>
    /// <remarks>
    ///     Sample request:
    ///     DELETE api/users/delete-profile-picture?picture=default_profile.jpg
    /// </remarks>
    [HttpDelete("delete-profile-picture")]
    [Authorize(Policy = Permissions.UserEditProfile)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status200OK)]
    public async Task<ActionResult> DeleteProfilePicture([FromQuery] string picture)
    {
        //receive userId by JWT Token 
        var claimUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        if (claimUserId is null || !int.TryParse(claimUserId, out var userId))
            return Unauthorized();

        var result = await _mediator.Send(new DeleteUserPictureCommand
        {
            Picture = picture,
            UserId = userId
        });
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    ///     Retrieves a paginated list of users.
    /// </summary>
    /// <param name="pagination">
    ///     Pagination parameters including page number and page size.
    /// </param>
    /// <returns>
    ///     Returns a <see cref="PagedResultDto{GetUsersDto}" /> containing the list of users.
    ///     If no users are found, returns <see cref="StatusCodes.Status204NoContent" />.
    /// </returns>
    /// <remarks>
    ///     Sample request:
    ///     GET api/users?pageNumber=1&pageSize=10
    /// </remarks>
    [HttpGet]
    [Authorize(Policy = Permissions.UserReadList)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(PagedResultDto<GetUsersDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PagedResultDto<GetUsersDto>>> Get([FromQuery] PaginationDto pagination)
    {
        var users = await
            _mediator.Send(new GetUsersListRequest { Pagination = pagination });
        if (!users.Items.Any())
            return NoContent();
        return Ok(users);
    }
}