using HR_Management.Application.DTOs.LeaveType;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.LeaveTypes.Requests.Queries;

public class GetLeaveTypeDetailRequest : IRequest<ResultDto<LeaveTypeDto>>
{
    public int Id { get; set; }
}