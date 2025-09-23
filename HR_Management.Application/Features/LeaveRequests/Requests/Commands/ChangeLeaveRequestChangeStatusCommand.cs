using HR_Management.Application.DTOs.LeaveRequest.ChangeLeaveRequestApproval;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.LeaveRequests.Requests.Commands;

public class ChangeLeaveRequestChangeStatusCommand : IRequest<ResultDto>
{
    public ChangeLeaveRequestChangeStatusDto ChangeLeaveRequestChangeStatusDto { get; set; }
    public int UserId { get; set; }
}