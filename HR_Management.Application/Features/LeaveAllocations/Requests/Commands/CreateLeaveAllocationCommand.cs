using HR_Management.Application.DTOs.LeaveAllocation;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.LeaveAllocations.Requests.Commands;

public class CreateLeaveAllocationCommand : IRequest<ResultDto<int>>
{
    public CreateLeaveAllocationDto CreateLeaveAllocationDto { get; set; }
}