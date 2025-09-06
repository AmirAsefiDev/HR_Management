using HR_Management.Application.DTOs.LeaveType;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.LeaveTypes.Requests.Commands;

public class CreateLeaveTypeCommand : IRequest<ResultDto<int>>
{
    public CreateLeaveTypeDto LeaveTypeDto { get; set; }
}