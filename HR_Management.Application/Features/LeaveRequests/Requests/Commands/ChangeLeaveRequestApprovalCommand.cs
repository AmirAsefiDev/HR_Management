using HR_Management.Application.DTOs.LeaveRequest.ChangeLeaveRequestApproval;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.LeaveRequests.Requests.Commands;

public class ChangeLeaveRequestApprovalCommand : IRequest<ResultDto>
{
    public ChangeLeaveRequestApprovalDto ChangeLeaveRequestApprovalDto { get; set; }
}