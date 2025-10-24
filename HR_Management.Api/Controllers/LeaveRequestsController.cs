using System.Security.Claims;
using HR_Management.Application.DTOs.LeaveRequest;
using HR_Management.Application.DTOs.LeaveRequest.ChangeLeaveRequestApproval;
using HR_Management.Application.DTOs.LeaveRequest.CreateLeaveRequest;
using HR_Management.Application.DTOs.LeaveRequestStatusHistory;
using HR_Management.Application.Features.LeaveRequests.Requests.Commands;
using HR_Management.Application.Features.LeaveRequests.Requests.Queries;
using HR_Management.Application.Features.LeaveRequestStatusHistories.Requests.Queries;
using HR_Management.Common;
using HR_Management.Common.Pagination;
using HR_Management.Common.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR_Management.Api.Controllers;

[Route("api/leave-requests")]
[ApiController]
public class LeaveRequestsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveRequestsController(IMediator mediator)
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
    ///     Sample request: GET: api/leave-requests
    /// </remarks>
    [HttpGet]
    [Authorize(Policy = Permissions.LeaveRequestReadList)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(PagedResultDto<LeaveRequestListDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PagedResultDto<LeaveRequestListDto>>> Get([FromQuery] PaginationDto pagination)
    {
        var leaveRequests = await
            _mediator.Send(new GetLeaveRequestsListRequest { Pagination = pagination });
        if (!leaveRequests.Items.Any())
            return NoContent();
        return Ok(leaveRequests);
    }

    /// <summary>
    ///     Retrieves all leave requests submitted by the currently authenticated user.
    /// </summary>
    /// <returns>
    /// </returns>
    /// <remarks>
    ///     Sample request: GET: api/leave-requests/me
    /// </remarks>
    [HttpGet("me")]
    [Authorize(Policy = Permissions.MyLeaveRequestsList)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(PagedResultDto<LeaveRequestDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetMyRequest([FromQuery] PaginationDto pagination)
    {
        //receive userId by Token
        var claimUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        if (string.IsNullOrEmpty(claimUserId))
            return Unauthorized();
        var userId = int.Parse(claimUserId);

        var result = await _mediator.Send(
            new GetMyLeaveRequestsRequest
            {
                UserId = userId,
                Pagination = pagination
            });
        return Ok(result);
    }

    /// <summary>
    ///     Retrieves the details of a specific leave request by its Id
    /// </summary>
    /// <param name="id">The Id of the leave request</param>
    /// <returns>
    ///     - 200 (Ok) : Leave request found
    ///     - 400 (BadRequest) : Invalid Id
    ///     - 404 (NotFound) : leave request couldn't find
    ///     - 500 (InternalServerError) : An unexpected error occurred
    /// </returns>
    /// <remarks>
    ///     Sample request:
    ///     GET api/leave-requests/5
    /// </remarks>
    [HttpGet("{id}")]
    [Authorize(Policy = Permissions.LeaveRequestRead)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ResultDto<LeaveRequestDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultDto<LeaveRequestDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(LeaveRequestDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Get(int id)
    {
        var result = await _mediator.Send(new GetLeaveRequestDetailRequest { Id = id });
        return StatusCode(result.StatusCode, result.Data);
    }

    /// <summary>
    ///     Retrieves all status history entries of leave request by its Id.
    /// </summary>
    /// <param name="id">
    ///     The Id of the leave request.
    /// </param>
    /// <returns></returns>
    /// <remarks>
    ///     Sample request:
    ///     GET api/leave-requests/5/status-history
    /// </remarks>
    [HttpGet("{id}/status-history")]
    [Authorize(Policy = Permissions.LeaveRequestStatusHistoryRead)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(PagedResultDto<LeaveRequestStatusHistoryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PagedResultDto<LeaveRequestStatusHistoryDto>>> GetLeaveRequestStatusHistories(int id,
        [FromQuery] PaginationDto pagination)
    {
        var leaveRequestStatusHistories = await _mediator.Send(
            new GetLeaveRequestStatusHistoriesByRequestIdRequest
            {
                LeaveRequestId = id,
                Pagination = pagination
            });
        if (!leaveRequestStatusHistories.Items.Any())
            return NoContent();
        return Ok(leaveRequestStatusHistories);
    }

    /// <summary>
    ///     Creates a new leave request.
    /// </summary>
    /// <param name="request">
    ///     The leave request data to create, including start date, end date, leave type,leave status , etc.
    ///     The request containing the new leave measure type.
    ///     Allowed values: DayBased(1), HourBased(2).
    /// </param>
    /// <returns>
    ///     - 201 (Created): Leave request created successfully
    ///     - 400 (BadRequest): Validation failed or invalid input
    ///     - 500 (InternalServerError): An unexpected error occurred
    /// </returns>
    /// <remarks>
    ///     Sample request:
    ///     POST api/leave-requests
    ///     {
    ///     "startDate":"2025-09-10",
    ///     "endDate":"2025-09-10",
    ///     "leaveTypeId":1,
    ///     "leaveStatusId":1,
    ///     "requestComments":"Need a leave for personal work",
    ///     "leaveMeasureType":1
    ///     }
    /// </remarks>
    [HttpPost]
    [Authorize(Policy = Permissions.LeaveRequestCreate)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ResultDto<int>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultDto<int>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Post(CreateLeaveRequestRequestDto request)
    {
        //receive userId by Token
        var claimUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        if (string.IsNullOrEmpty(claimUserId))
            return Unauthorized();
        var userId = int.Parse(claimUserId);

        var command = new CreateLeaveRequestCommand
        {
            CreateLeaveRequestDto = new CreateLeaveRequestDto
            {
                EndDate = request.EndDate,
                LeaveStatusId = 1,
                LeaveTypeId = request.LeaveTypeId,
                RequestComments = request.RequestComments,
                StartDate = request.StartDate,
                UserId = userId,
                LeaveMeasureType = request.LeaveMeasureType
            }
        };
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    ///     Updates the details of an existing leave request.
    /// </summary>
    /// <param name="id">
    ///     The Id of the leave request to be updated.
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
    ///     PUT /api/leave-requests/5
    ///     {
    ///     "startDate": "2025-09-12",
    ///     "endDate": "2025-09-15",
    ///     "leaveTypeId": 1,
    ///     "leaveStatusId": 2
    ///     }
    /// </remarks>
    [HttpPut("{id}")]
    [Authorize(Policy = Permissions.LeaveRequestUpdate)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status403Forbidden)]
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
    ///     PATCH /api/leave-requests/5/change-status
    ///     {
    ///     "approvalStatus": "Approved"
    ///     }
    /// </remarks>
    [HttpPatch("{id}/change-status")]
    [Authorize(Policy = Permissions.LeaveRequestChangeStatus)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ChangeStatus(int id,
        [FromBody] ChangeLeaveRequestChangeStatusRequestDto request)
    {
        //receive userId by Token
        var claimUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        if (string.IsNullOrEmpty(claimUserId))
            return Unauthorized();
        var userId = int.Parse(claimUserId);
        var command = new ChangeLeaveRequestChangeStatusCommand
        {
            ChangeLeaveRequestChangeStatusDto = new ChangeLeaveRequestChangeStatusDto
            {
                Id = id,
                approvalStatus = request.ApprovalStatus,
                Comment = request.Comment
            },
            UserId = userId
        };
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    /// <summary>
    ///     Deletes an existing leave request by its Id.
    /// </summary>
    /// <param name="id">
    ///     The Id of the leave request to be deleted
    /// </param>
    /// <returns>
    ///     Returns a <see cref="ResultDto" /> wrapped in an <see cref="ActionResult" />:
    ///     - 200 (OK) : Leave request successfully deleted
    ///     - 400 (BadRequest) : Invalid request (e.g, Id isn't valid)
    ///     - 500 (InternalServerError) : An unexpected error occurred
    /// </returns>
    /// <remarks>
    ///     Sample request:
    ///     DELETE /api/leave-requests/5
    /// </remarks>
    [HttpDelete("{id}")]
    [Authorize(Policy = Permissions.LeaveRequestDelete)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status409Conflict)]
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