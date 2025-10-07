using HR_Management.Application.DTOs.LeaveStatus;
using HR_Management.Common.Pagination;
using MediatR;

namespace HR_Management.Application.Features.LeaveStatuses.Requests.Queries;

public class GetLeaveStatusListRequest : IRequest<PagedResultDto<LeaveStatusDto>>
{
    public PaginationDto Pagination { get; set; }
}