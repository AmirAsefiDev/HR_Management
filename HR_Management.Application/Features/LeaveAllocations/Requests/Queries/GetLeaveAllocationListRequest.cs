using HR_Management.Application.DTOs.LeaveAllocation;
using HR_Management.Common.Pagination;
using MediatR;

namespace HR_Management.Application.Features.LeaveAllocations.Requests.Queries;

public class GetLeaveAllocationListRequest : IRequest<PagedResultDto<LeaveAllocationDto>>
{
    public PaginationDto Pagination { get; set; }
}