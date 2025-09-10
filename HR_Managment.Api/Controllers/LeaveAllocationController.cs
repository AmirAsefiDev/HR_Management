using HR_Management.Application.DTOs.LeaveAllocation;
using HR_Management.Application.DTOs.LeaveAllocation.UpdateLeaveAllocation;
using HR_Management.Application.Features.LeaveAllocations.Requests.Commands;
using HR_Management.Application.Features.LeaveAllocations.Requests.Queries;
using HR_Management.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR_Management.Api.Controllers;

[Route("api/leave-allocation")]
[ApiController]
[Authorize]
public class LeaveAllocationController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveAllocationController(IMediator mediator)
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
    ///     GET: api/leave-allocation
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(typeof(List<LeaveAllocationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<LeaveAllocationDto>>> Get()
    {
        var leaveAllocations = await
            _mediator.Send(new GetLeaveAllocationListRequest());
        if (!leaveAllocations.Any())
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
    ///     GET api/leave-allocation/5
    /// </remarks>
    [HttpGet("{id}")]
    public async Task<ActionResult> Get(int id)
    {
        var result = await _mediator.Send(new GetLeaveAllocationDetailRequest { Id = id });
        return StatusCode(result.StatusCode, result);
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
    ///     POST api/leave-allocation
    ///     {
    ///     "leaveTypeId":1,
    ///     "period":1,
    ///     "numberOfDays:1
    ///     }
    /// </remarks>
    [HttpPost]
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
    ///     Updates the details of an existing leave allocation
    /// </summary>
    /// <param name="id">
    ///     The Id of the leave allocation to be updated
    /// </param>
    /// <param name="request"></param>
    /// <returns>
    ///     - 200 (OK): Leave allocation updated successfully
    ///     - 400 (BadRequest): Validation failed or invalid input
    ///     - 500 (InternalServerError): An unexpected error occurred
    /// </returns>
    /// <remarks>
    ///     Sample request:
    ///     PUT api/leave-allocation/5
    ///     {
    ///     "leaveTypeId":1,
    ///     "period":1,
    ///     "numberOfDays":1
    ///     }
    /// </remarks>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Put(int id, [FromBody] UpdateLeaveAllocationRequestDto request)
    {
        var command = new UpdateLeaveAllocationCommand
        {
            UpdateLeaveAllocationDto = new UpdateLeaveAllocationDto
            {
                Id = id,
                LeaveTypeId = request.LeaveTypeId,
                NumberOfDays = request.NumberOfDays,
                Period = request.Period
            }
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
    ///     DELETE api/leave-allocation/5
    /// </remarks>
    [HttpDelete("{id}")]
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
}