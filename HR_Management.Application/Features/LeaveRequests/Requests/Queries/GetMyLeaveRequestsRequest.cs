using HR_Management.Application.DTOs.LeaveRequest;
using HR_Management.Common.Pagination;
using MediatR;

namespace HR_Management.Application.Features.LeaveRequests.Requests.Queries;

public class GetMyLeaveRequestsRequest : IRequest<PagedResultDto<LeaveRequestDto>>
{
    public PaginationDto Pagination { get; set; }
    public int UserId { get; set; }
}