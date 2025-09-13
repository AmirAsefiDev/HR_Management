using AutoMapper;
using HR_Management.Application.Contracts.Persistence.Context;
using HR_Management.Application.DTOs.LeaveRequest;
using HR_Management.Application.Features.LeaveRequests.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HR_Management.Application.Features.LeaveRequests.Handlers.Queries;

public class GetMyLeaveRequestsRequestHandler : IRequestHandler<GetMyLeaveRequestsRequest, List<LeaveRequestDto>>
{
    private readonly ILeaveManagementDbContext _context;
    private readonly IMapper _mapper;

    public GetMyLeaveRequestsRequestHandler(ILeaveManagementDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<LeaveRequestDto>> Handle(GetMyLeaveRequestsRequest request,
        CancellationToken cancellationToken)
    {
        var leaveRequests = await _context.LeaveRequests
            .Include(l => l.LeaveType)
            .Include(l => l.LeaveStatus)
            .Where(l => l.UserId == request.UserId)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<LeaveRequestDto>>(leaveRequests);
    }
}