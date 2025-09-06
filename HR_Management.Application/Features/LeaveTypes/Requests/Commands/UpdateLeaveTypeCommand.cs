using HR_Management.Application.DTOs.LeaveType;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.LeaveTypes.Requests.Commands;

public class UpdateLeaveTypeCommand : IRequest<ResultDto>
{
    public LeaveTypeDto LeaveTypeDto { get; set; }
}