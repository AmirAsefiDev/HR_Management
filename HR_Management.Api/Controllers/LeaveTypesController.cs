using HR_Management.Application.DTOs.LeaveType;
using HR_Management.Application.Features.LeaveTypes.Requests.Commands;
using HR_Management.Application.Features.LeaveTypes.Requests.Queries;
using HR_Management.Common;
using HR_Management.Common.Pagination;
using HR_Management.Common.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR_Management.Api.Controllers;

[Route("api/leave-types")]
[ApiController]
public class LeaveTypesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     receive all leave types
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    ///     SampleRequest: GET api/leave-types/
    /// </remarks>
    [HttpGet]
    [Authorize(Policy = Permissions.LeaveTypeReadList)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(PagedResultDto<LeaveTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PagedResultDto<LeaveTypeDto>>> Get([FromQuery] PaginationDto pagination)
    {
        var leaveTypes = await _mediator.Send(
            new GetLeaveTypeListRequest
            {
                Pagination = pagination
            });
        return Ok(leaveTypes);
    }

    /// <summary>
    ///     receive a leave type by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <remarks>
    ///     SampleRequest: GET api/leave-types/5
    /// </remarks>
    [HttpGet("{id}")]
    [Authorize(Policy = Permissions.LeaveTypeRead)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(LeaveTypeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<LeaveTypeDto>> Get(int id)
    {
        var leaveType = await _mediator.Send(new GetLeaveTypeDetailRequest { Id = id });
        return Ok(leaveType);
    }

    /// <summary>
    ///     Add new Leave type in Database
    /// </summary>
    /// <param name="leaveType"></param>
    /// <returns></returns>
    /// <remarks>
    ///     SampleRequest:  POST api/leave-types
    /// </remarks>
    [HttpPost]
    [Authorize(Policy = Permissions.LeaveTypeCreate)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultDto<int>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Post([FromBody] CreateLeaveTypeDto leaveType)
    {
        var command = new CreateLeaveTypeCommand
        {
            LeaveTypeDto = leaveType
        };
        var response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }

    /// <summary>
    ///     Edit Leave Type info
    /// </summary>
    /// <param name="id"></param>
    /// <param name="leaveType"></param>
    /// <returns></returns>
    /// <remarks>
    ///     SampleRequest: PUT api/leave-types/5
    /// </remarks>
    [HttpPut("{id}")]
    [Authorize(Policy = Permissions.LeaveTypeUpdate)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Put(int id, [FromBody] LeaveTypeDto leaveType)
    {
        leaveType.Id = id;
        var command = new UpdateLeaveTypeCommand
        {
            LeaveTypeDto = leaveType
        };
        var response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }

    /// <summary>
    ///     Delete Leave Type with id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <remarks>
    ///     SampleRequest: DELETE api/leave-types/5
    /// </remarks>
    [HttpDelete("{id}")]
    [Authorize(Policy = Permissions.LeaveTypeDelete)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteLeaveTypeCommand { Id = id };
        var response = await _mediator.Send(command);
        return StatusCode(response.StatusCode, response);
    }
}