using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.LeaveRequests.Requests.Commands;

public class DeleteLeaveRequestCommand : IRequest<ResultDto>
{
    public int Id { get; set; }
}