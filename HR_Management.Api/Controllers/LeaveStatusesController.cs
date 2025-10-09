using HR_Management.Application.DTOs.LeaveStatus;
using HR_Management.Application.Features.LeaveStatuses.Requests.Commands;
using HR_Management.Application.Features.LeaveStatuses.Requests.Queries;
using HR_Management.Common;
using HR_Management.Common.Pagination;
using HR_Management.Common.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR_Management.Api.Controllers;

[Route("api/leave-statuses")]
[ApiController]
public class LeaveStatusesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveStatusesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Retrieves list of leave statuses
    /// </summary>
    /// <returns>
    ///     - 200 (OK) : List of leave statuses
    ///     - 204 (NoContent) : No leave statuses is available to retrieve
    ///     - 401 (UnAuthorized) : User token expired,they have to log in again or refresh the token.
    ///     - 500 (InternalServerError) : An unexpected error occurred
    /// </returns>
    /// <remarks>
    ///     Sample request: GET: api/leave-statuses
    /// </remarks>
    [HttpGet]
    [Authorize(Policy = Permissions.LeaveStatusReadList)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(PagedResultDto<LeaveStatusDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PagedResultDto<LeaveStatusDto>>> Get([FromQuery] PaginationDto pagination)
    {
        var leaveRequests = await
            _mediator.Send(new GetLeaveStatusListRequest { Pagination = pagination });
        if (!leaveRequests.Items.Any())
            return NoContent();
        return Ok(leaveRequests);
    }

    /// <summary>
    ///     Retrieves list of leave status for selection for example in select,dropdown
    /// </summary>
    /// <returns>
    /// </returns>
    /// <remarks>
    ///     Sample request: GET: api/leave-statuses/selection
    /// </remarks>
    [HttpGet("selection")]
    [Authorize(Policy = Permissions.LeaveStatusReadListSelection)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(List<LeaveStatusDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<LeaveStatusDto>>> Get()
    {
        var result = await _mediator.Send(new GetLeaveStatusListSelectionRequest());
        if (!result.Any())
            return NoContent();
        return Ok(result);
    }

    /// <summary>
    ///     Retrieves the details of a specific leave status by its Id
    /// </summary>
    /// <param name="id">The Id of the leave status</param>
    /// <returns>
    ///     - 200 (Ok) : Leave status found
    ///     - 400 (BadRequest) : Invalid Id
    ///     - 404 (NotFound) : leave request couldn't find
    ///     - 500 (InternalServerError) : An unexpected error occurred
    /// </returns>
    /// <remarks>
    ///     Sample request:
    ///     GET api/leave-statuses/5
    /// </remarks>
    [HttpGet("{id}")]
    [Authorize(Policy = Permissions.LeaveStatusRead)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ResultDto<LeaveStatusDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultDto<LeaveStatusDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(LeaveStatusDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Get(int id)
    {
        var result = await _mediator.Send(new GetLeaveStatusDetailRequest { Id = id });
        return StatusCode(result.StatusCode, result.Data);
    }


    /// <summary>
    ///     Creates a new leave status.
    /// </summary>
    /// <param name="request">
    ///     The leave status data to create, including name, description
    /// </param>
    /// <returns>
    ///     - 201 (Created): Leave status created successfully
    ///     - 400 (BadRequest): Validation failed or invalid input
    ///     - 500 (InternalServerError): An unexpected error occurred
    /// </returns>
    /// <remarks>
    ///     Sample request:
    ///     POST api/leave-statuses
    ///     {
    ///     "name":"emergency",
    ///     "description":"(Optional)ex:Using for accepted leave request.",
    ///     }
    /// </remarks>
    [HttpPost]
    [Authorize(Policy = Permissions.LeaveStatusCreate)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ResultDto<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultDto<int>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Post(CreateLeaveStatusDto request)
    {
        var command = new CreateLeaveStatusCommand
        {
            CreateLeaveStatusDto = request
        };
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    ///     Updates the details of an existing leave request.
    /// </summary>
    /// <param name="request">
    ///     The updated leave status data (id,name, description).
    /// </param>
    /// <returns>
    ///     Returns a <see cref="ResultDto" /> wrapped in an <see cref="ActionResult" />:
    ///     - 200 (OK): Leave status updated successfully
    ///     - 400 (BadRequest): Validation failed or invalid input
    ///     - 500 (InternalServerError): An unexpected error occurred
    /// </returns>
    /// <remarks>
    ///     Sample request:
    ///     PUT /api/leave-status
    ///     {
    ///     "id":1,
    ///     "name":"emergency",
    ///     "description":"(Optional)ex:Using for accepted leave request.",
    ///     }
    /// </remarks>
    [HttpPut]
    [Authorize(Policy = Permissions.LeaveStatusUpdate)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Put([FromBody] UpdateLeaveStatusDto request)
    {
        var command = new UpdateLeaveStatusCommand
        {
            UpdateLeaveStatusDto = request
        };
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }


    /// <summary>
    ///     Deletes an existing leave status by its Id.
    /// </summary>
    /// <param name="id">
    ///     The Id of the leave status to be deleted
    /// </param>
    /// <returns>
    ///     Returns a <see cref="ResultDto" /> wrapped in an <see cref="ActionResult" />:
    ///     - 200 (OK) : Leave status successfully deleted
    ///     - 400 (BadRequest) : Invalid request (e.g, Id isn't valid)
    ///     - 500 (InternalServerError) : An unexpected error occurred
    /// </returns>
    /// <remarks>
    ///     Sample request:
    ///     DELETE api/leave-statuses/5
    /// </remarks>
    [HttpDelete("{id}")]
    [Authorize(Policy = Permissions.LeaveStatusDelete)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteLeaveStatusCommand
        {
            Id = id
        };
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }
}