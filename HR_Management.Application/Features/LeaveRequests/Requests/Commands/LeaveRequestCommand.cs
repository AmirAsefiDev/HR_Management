using HR_Management.Application.DTOs.LeaveRequest;
using MediatR;

namespace HR_Management.Application.Features.LeaveRequests.Requests.Commands;

public class LeaveRequestCommand : IRequest<int>
{
    public LeaveRequestDto LeaveRequestDto { get; set; }
}