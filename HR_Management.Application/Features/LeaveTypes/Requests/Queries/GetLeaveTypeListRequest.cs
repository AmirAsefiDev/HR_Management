using HR_Management.Application.DTOs.LeaveType;
using HR_Management.Common.Pagination;
using MediatR;

namespace HR_Management.Application.Features.LeaveTypes.Requests.Queries;

public class GetLeaveTypeListRequest : IRequest<PagedResultDto<LeaveTypeDto>>
{
    public PaginationDto Pagination { get; set; }
}