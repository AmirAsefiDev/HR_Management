using HR_Management.Application.DTOs.LeaveAllocation;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.LeaveAllocations.Requests.Queries;

public class GetLeaveAllocationDetailRequest : IRequest<ResultDto<LeaveAllocationDto>>
{
    public int Id { get; set; }
}