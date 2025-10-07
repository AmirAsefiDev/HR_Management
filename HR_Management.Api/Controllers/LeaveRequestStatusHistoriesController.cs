using HR_Management.Application.DTOs.LeaveRequestStatusHistory;
using HR_Management.Application.Features.LeaveRequestStatusHistories.Requests.Queries;
using HR_Management.Common;
using HR_Management.Common.Pagination;
using HR_Management.Common.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR_Management.Api.Controllers;

[Route("api/leave-request-status-histories")]
[ApiController]
public class LeaveRequestStatusHistoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveRequestStatusHistoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Retrieves all leave request status histories as a report.
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    ///     Sample Request: GET: api/leave-request-status-histories
    /// </remarks>
    [HttpGet]
    [Authorize(Policy = Permissions.LeaveRequestStatusHistoryReadList)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(PagedResultDto<LeaveRequestStatusHistoryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultDto), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PagedResultDto<LeaveRequestStatusHistoryDto>>> Get(
        [FromQuery] PaginationDto Pagination)
    {
        var leaveRequestStatusHistories = await _mediator.Send(
            new GetLeaveRequestStatusHistoriesListRequest
            {
                Pagination = Pagination
            });

        return leaveRequestStatusHistories;
    }
}