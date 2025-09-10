using HR_Management.Application.DTOs.LeaveRequest;
using HR_Management.Application.DTOs.LeaveRequest.ChangeLeaveRequestApproval;
using HR_Management.Application.Features.LeaveRequests.Requests.Commands;
using HR_Management.Application.Features.LeaveRequests.Requests.Queries;
using HR_Management.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR_Management.Api.Controllers;

[Route("api/leave-request")]
[ApiController]
[Authorize]
public class LeaveRequestController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveRequestController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Retrieves list of leave requests
    /// </summary>
    /// <returns>
    ///     - 200 (OK) : List of leave requests
    ///     - 204 (NoContent) : No leave requests is available to retrieve
    ///     - 500 (InternalServerError) : An unexpected error occurred
    /// </returns>
    /// <remarks>
    ///     Sample request: GET: api/leave-request
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(typeof(List<LeaveRequestDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<LeaveRequestDto>>> Get()
    {
        var leaveRequests = await _mediator.Send(new GetLeaveRequestsListRequest());
        if (!leaveRequests.Any())
            return NoContent();
        return Ok(leaveRequests);
    }

    /// <summary>
    ///     Retrieves the details of a specific leave request by its Id
    /// </summary>
    /// <param name="id">The Id of the leave request</param>
    /// <returns>
    ///     - 200 (Ok) : Leave request found
    ///     - 400 (BadRequest) : Invalid Id
    ///     - 500 (InternalServerError) : An unexpected error occurred
    /// </returns>
    /// <remarks>
    ///     Sample request:
    ///     GET api/leave-request/5
    /// </remarks>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResultDto<LeaveRequestDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultDto<LeaveRequestDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Get(int id)
    {
        var result = await _mediator.Send(new GetLeaveRequestDetailRequest { Id = id });
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    ///     Creates a new leave request.
    /// </summary>
    /// <param name="request">
    ///     The leave request data to create, including start date, end date, leave type,leave status , etc.
    /// </param>
    /// <returns></returns>
    /// <remarks>
    ///     Sample request:
    ///     POST api/leave-request
    ///     {
    ///     "startDate":"2025-09-10",
    ///     "endDate":"2025-09-10",
    ///     "leaveTypeId":1,
    ///     "leaveStatusId":1,
    ///     "requestComments":"Need a leave for personal work"
    ///     }
    /// </remarks>
    // POST api/leave-request
    [HttpPost]
    [ProducesResponseType(typeof(ResultDto<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultDto<int>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Post(CreateLeaveRequestDto request)
    {
        //receive userId by Token
        var claimUserId = User.FindFirst("UserId").Value;
        if (string.IsNullOrEmpty(claimUserId))
            return Unauthorized();
        var userId = int.Parse(claimUserId);

        var command = new CreateLeaveRequestCommand
        {
            CreateLeaveRequestDto = request,
            UserId = userId
        };
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    ///     Updates the details of an existing leave request.
    /// </summary>
    /// <param name="id">
    ///     The unique identifier of the leave request to be updated.
    /// </param>
    /// <param name="request">
    ///     The updated leave request data (start date, end date, leave type, etc.).
    /// </param>
    /// <returns>
    ///     Returns a <see cref="ResultDto" /> wrapped in an <see cref="ActionResult" />:
    ///     - 200 (OK): Leave request updated successfully
    ///     - 400 (BadRequest): Validation failed or invalid input
    ///     - 500 (InternalServerError): An unexpected error occurred
    /// </returns>
    /// <remarks>
    ///     Sample request:
    ///     PUT /api/leave-request/5
    ///     {
    ///     "startDate": "2025-09-12",
    ///     "endDate": "2025-09-15",
    ///     "leaveTypeId": 1,
    ///     "leaveStatusId": 2
    ///     }
    /// </remarks>
    // PUT api/leave-request/5
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Put(int id, [FromBody] UpdateLeaveRequestDto request)
    {
        var command = new UpdateLeaveRequestCommand
        {
            Id = id,
            UpdateLeaveRequestDto = request
        };
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    ///     Updates the approval status of a specific leave request.
    /// </summary>
    /// <param name="id">The unique identifier of the leave request.</param>
    /// <param name="request">
    ///     The request containing the new approval status.
    ///     Allowed values: Pending(1), Approved(2), Rejected(3), Cancelled(4).
    /// </param>
    /// <returns>
    ///     Returns a <see cref="ResultDto" /> wrapped in an <see cref="ActionResult" />:
    ///     - 200 (OK): Status successfully updated
    ///     - 400 (BadRequest): Invalid input data
    ///     - 500 (InternalServerError): An unexpected error occurred
    /// </returns>
    /// <remarks>
    ///     Sample request:
    ///     PATCH /api/leave-request/5/change-approval-status
    ///     {
    ///     "approvalStatus": "Approved"
    ///     }
    /// </remarks>
    [HttpPatch("{id}/change-approval-status")]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ChangeApprovalStatus(int id,
        [FromBody] ChangeLeaveRequestApprovalRequestDto request)
    {
        var command = new ChangeLeaveRequestApprovalCommand
        {
            ChangeLeaveRequestApprovalDto = new ChangeLeaveRequestApprovalDto
            {
                Id = id,
                approvalStatus = request.ApprovalStatus
            }
        };
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    ///     Deletes an existing leave request by its unique identifier.
    /// </summary>
    /// <param name="id">
    ///     The unique identifier of the leave request to be deleted
    /// </param>
    /// <returns>
    ///     Returns a <see cref="ResultDto" /> wrapped in an <see cref="ActionResult" />:
    ///     - 200 (OK) : Leave request successfully deleted
    ///     - 400 (BadRequest) : Invalid request (e.g, Id is not valid)
    ///     - 500 (InternalServerError) : An unexpected error occurred
    /// </returns>
    /// <remarks>
    ///     Sample request:
    ///     DELETE /api/leave-request/5
    /// </remarks>
    // DELETE api/leave-request/5
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteLeaveRequestCommand
        {
            Id = id
        };
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }
}