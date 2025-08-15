using HR_Management.Application.DTOs.LeaveRequest;
using MediatR;

namespace HR_Management.Application.Features.LeaveRequests.Requests.Commands;

public class CreateLeaveRequestCommand : IRequest<int>
{
    public CreateLeaveRequestDto CreateLeaveRequestDto { get; set; }
}