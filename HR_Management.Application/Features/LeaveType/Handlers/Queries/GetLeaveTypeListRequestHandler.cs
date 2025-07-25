using HR_Management.Application.DTOs;
using HR_Management.Application.Features.LeaveType.Request;
using MediatR;

namespace HR_Management.Application.Features.LeaveType.Handlers.Queries;

public class GetLeaveTypeListRequestHandler :
    IRequestHandler<GetLeaveTypeListRequest, List<LeaveTypeDto>>
{
    public Task<List<LeaveTypeDto>> Handle(GetLeaveTypeListRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}