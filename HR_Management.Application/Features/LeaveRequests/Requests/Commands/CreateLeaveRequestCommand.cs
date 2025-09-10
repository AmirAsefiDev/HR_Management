using HR_Management.Application.DTOs.LeaveRequest;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.LeaveRequests.Requests.Commands;

public class CreateLeaveRequestCommand : IRequest<ResultDto<int>>
{
    public CreateLeaveRequestDto CreateLeaveRequestDto { get; set; }
    public int UserId { get; set; }
}