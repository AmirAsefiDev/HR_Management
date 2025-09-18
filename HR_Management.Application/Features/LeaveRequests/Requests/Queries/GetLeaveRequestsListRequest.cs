using HR_Management.Application.DTOs.LeaveRequest;
using HR_Management.Common.Pagination;
using MediatR;

namespace HR_Management.Application.Features.LeaveRequests.Requests.Queries;

public class GetLeaveRequestsListRequest : IRequest<PagedResultDto<LeaveRequestListDto>>
{
    public PaginationDto Pagination { get; set; }
}