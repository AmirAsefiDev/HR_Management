using HR_Management.Application.DTOs.LeaveRequest;
using MediatR;

namespace HR_Management.Application.Features.LeaveRequests.Requests.Queries;

public class GetMyLeaveRequestsRequest : IRequest<List<LeaveRequestDto>>
{
    public int UserId { get; set; }
}