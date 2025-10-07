using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.LeaveStatuses.Requests.Commands;

public class DeleteLeaveStatusCommand : IRequest<ResultDto>
{
    public int Id { get; set; }
}