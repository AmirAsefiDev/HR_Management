using HR_Management.Application.DTOs.LeaveRequest;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.LeaveRequests.Requests.Commands;

public class UpdateLeaveRequestCommand : IRequest<ResultDto>
{
    public int Id { get; set; }
    public UpdateLeaveRequestDto UpdateLeaveRequestDto { get; set; }
}