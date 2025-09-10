using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.LeaveAllocations.Requests.Commands;

public class DeleteLeaveAllocationCommand : IRequest<ResultDto>
{
    public int Id { get; set; }
}