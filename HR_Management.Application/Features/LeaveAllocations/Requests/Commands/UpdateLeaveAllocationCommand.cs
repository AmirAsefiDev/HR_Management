using HR_Management.Application.DTOs.LeaveAllocation.UpdateLeaveAllocation;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.LeaveAllocations.Requests.Commands;

public class UpdateLeaveAllocationCommand : IRequest<ResultDto>
{
    public UpdateLeaveAllocationDto UpdateLeaveAllocationDto { get; set; }
}