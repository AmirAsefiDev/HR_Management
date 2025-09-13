using HR_Management.Application.DTOs.LeaveRequestStatusHistory;
using MediatR;

namespace HR_Management.Application.Features.LeaveRequestStatusHistory.Requests.Queries;

public class GetLeaveRequestStatusHistoryRequest : IRequest<List<LeaveRequestStatusHistoryDto>>
{
    public int LeaveRequestId { get; set; }
}