using HR_Management.Application.DTOs.LeaveStatus;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.LeaveStatuses.Requests.Commands;

public class CreateLeaveStatusCommand : IRequest<ResultDto<int>>
{
    public CreateLeaveStatusDto CreateLeaveStatusDto { get; set; }
}