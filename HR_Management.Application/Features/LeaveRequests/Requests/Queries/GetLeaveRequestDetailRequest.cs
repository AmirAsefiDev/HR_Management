using HR_Management.Application.DTOs.LeaveRequest;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.LeaveRequests.Requests.Queries;

public class GetLeaveRequestDetailRequest : IRequest<ResultDto<LeaveRequestDto>>
{
    public int Id { get; set; }
}