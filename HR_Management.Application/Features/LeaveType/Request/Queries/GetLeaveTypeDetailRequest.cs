using HR_Management.Application.DTOs;
using MediatR;

namespace HR_Management.Application.Features.LeaveType.Request.Queries;

public class GetLeaveTypeDetailRequest : IRequest<LeaveTypeDto>
{
    public int Id { get; set; }
}