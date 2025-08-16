using HR_Management.Application.DTOs.LeaveStatus;
using MediatR;

namespace HR_Management.Application.Features.LeaveStatuses.Requests.Commands;

public class UpdateLeaveStatusCommand : IRequest<Unit>
{
    public UpdateLeaveStatusDto UpdateLeaveStatusDto { get; set; }
}