using HR_Management.Application.DTOs.LeaveRequestStatusHistory;
using HR_Management.Common.Pagination;
using MediatR;

namespace HR_Management.Application.Features.LeaveRequestStatusHistory.Requests.Queries;

public class GetLeaveRequestStatusHistoriesListRequest : IRequest<PagedResultDto<LeaveRequestStatusHistoryDto>>
{
    public PaginationDto Pagination { get; set; }
}