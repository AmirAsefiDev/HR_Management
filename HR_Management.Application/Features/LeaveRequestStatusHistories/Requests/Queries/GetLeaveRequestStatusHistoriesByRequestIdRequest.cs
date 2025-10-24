using HR_Management.Application.DTOs.LeaveRequestStatusHistory;
using HR_Management.Common.Pagination;
using MediatR;

namespace HR_Management.Application.Features.LeaveRequestStatusHistories.Requests.Queries;

public class GetLeaveRequestStatusHistoriesByRequestIdRequest : IRequest<PagedResultDto<LeaveRequestStatusHistoryDto>>
{
    public int LeaveRequestId { get; set; }
    public PaginationDto Pagination { get; set; }
}