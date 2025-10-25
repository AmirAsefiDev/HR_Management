using HR_Management.Application.DTOs.LeaveAllocation;
using HR_Management.Application.Features.LeaveAllocations.Requests.Commands;
using HR_Management.Application.Features.LeaveAllocations.Requests.Queries;
using HR_Management.Common;
using HR_Management.Common.Pagination;
using HR_Management.Common.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR_Management.Api.Controllers;

[Route("api/leave-allocations")]
[ApiController]
public class LeaveAllocationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveAllocationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Retrieves list of leave allocations
    /// </summary>
    /// <returns>
    ///     - 200 (OK) : List of leave allocations
    ///     - 204 (NoContent) : No leave allocation is available to retrieve
    ///     - 500 (InternalServerError) : An unexpected error occurred
    /// </returns>
    /// <remarks>
    ///     Sample request:
    ///     GET: api/leave-allocations
    /// </remarks>
    [HttpGet]
    [Authorize(Policy = Permissions.LeaveAllocationReadList)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(PagedResultDto<LeaveAllocationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PagedResultDto<LeaveAllocationDto>>> Get([FromQuery] PaginationDto pagination)
    {
        var leaveAllocations = await
            _mediator.Send(new GetLeaveAllocationListRequest
            {
                Pagination = pagination
            });
        if (!leaveAllocations.Items.Any())
            return NoContent();
        return Ok(leaveAllocations);
    }

    /// <summary>
    ///     Retrieves the details of specific leave allocation by its Id
    /// </summary>
    /// <param name="id">
    ///     The Id of leave allocation
    /// </param>
    /// <returns>
    ///     - 200 (Ok) : Leave allocation found
    ///     - 400 (BadRequest) : Invalid Id
    ///     - 500 (InternalServerError) : An unexpected error occurred
    /// </returns>
    /// <remarks>
    ///     Sample request:
    ///     GET api/leave-allocations/5
    /// </remarks>
    [HttpGet("{id}")]
    [Authorize(Policy = Permissions.LeaveAllocationRead)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ResultDto<LeaveAllocationDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultDto<LeaveAllocationDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(LeaveAllocationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Get(int id)
    {
        var result = await _mediator.Send(new GetLeaveAllocationDetailRequest { Id = id });
        return StatusCode(result.StatusCode, result.Data);
    }

    /// <summary>
    ///     Creates a new leave allocation.
    /// </summary>
    /// <param name="request">
    ///     the leave allocation data to create: leaveTypeId,period,numberOfDays
    /// </param>
    /// <returns>
    ///     - 200 (OK): Leave allocation created successfully
    ///     - 400 (BadRequest): Validation failed or invalid input
    ///     - 500 (InternalServerError): An unexpected error occurred
    /// </returns>
    /// <remarks>
    ///     Sample request:
    ///     POST api/leave-allocations
    ///     {
    ///     "leaveTypeId":1,
    ///     "period":1,
    ///     "numberOfDays:1
    ///     }
    /// </remarks>
    [HttpPost]
    [Authorize(Policy = Permissions.LeaveAllocationCreate)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ResultDto<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultDto<int>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Post(CreateLeaveAllocationDto request)
    {
        var command = new CreateLeaveAllocationCommand
        {
            CreateLeaveAllocationDto = request
        };
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    ///     Deletes an existing leave allocation by its Id
    /// </summary>
    /// <param name="id">
    ///     The Id of the leave allocation to be deleted
    /// </param>
    /// <returns>
    ///     - 200 (OK) : Leave allocation successfully deleted.
    ///     - 400 (BadRequest) : Invalid request (e.g, Id isn't valid)
    ///     - 500 (InternalServerError) : An unexpected error occured
    /// </returns>
    /// <remarks>
    ///     Sample request:
    ///     DELETE api/leave-allocations/5
    /// </remarks>
    [HttpDelete("{id}")]
    [Authorize(Policy = Permissions.LeaveAllocationDelete)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteLeaveAllocationCommand
        {
            Id = id
        };
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    ///     Resets all existing leave allocations for every user in system and generates new allocations for the new
    ///     period(year).
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    ///     sample Request: POST api/leave-allocations/reset
    ///     This endpoint should typically be executed at the beginning of a new year
    ///     to renew all users leave allocations based on the current leave types and their default days.
    /// </remarks>
    [HttpPost("reset")]
    [Authorize(Policy = Permissions.LeaveAllocationReset)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ResetAllocations()
    {
        var result = await _mediator.Send(new RebuildLeaveAllocationsForNewYearCommand());
        return Ok(result);
    }
}