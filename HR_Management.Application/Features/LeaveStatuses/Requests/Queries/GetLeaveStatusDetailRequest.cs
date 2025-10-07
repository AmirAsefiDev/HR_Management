using HR_Management.Application.DTOs.LeaveStatus;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.LeaveStatuses.Requests.Queries;

public class GetLeaveStatusDetailRequest : IRequest<ResultDto<LeaveStatusDto>>
{
    public int Id { get; set; }
}