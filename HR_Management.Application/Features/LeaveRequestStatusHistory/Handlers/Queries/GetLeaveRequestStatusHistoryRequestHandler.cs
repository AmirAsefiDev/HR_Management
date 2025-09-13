using AutoMapper;
using HR_Management.Application.Contracts.Persistence.Context;
using HR_Management.Application.DTOs.LeaveRequestStatusHistory;
using HR_Management.Application.Features.LeaveRequestStatusHistory.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HR_Management.Application.Features.LeaveRequestStatusHistory.Handlers.Queries;

public class GetLeaveRequestStatusHistoryRequestHandler : IRequestHandler<GetLeaveRequestStatusHistoryRequest,
    List<LeaveRequestStatusHistoryDto>>
{
    private readonly ILeaveManagementDbContext _context;
    private readonly IMapper _mapper;

    public GetLeaveRequestStatusHistoryRequestHandler(ILeaveManagementDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<LeaveRequestStatusHistoryDto>> Handle(GetLeaveRequestStatusHistoryRequest request,
        CancellationToken cancellationToken)
    {
        var leaveRequestStatusHistories = await _context.LeaveRequestStatusHistories
            .Include(h => h.User)
            .Include(h => h.LeaveRequest)
            .Include(h => h.LeaveStatus)
            .Where(h => h.LeaveRequestId == request.LeaveRequestId)
            .ToListAsync(cancellationToken);
        return _mapper.Map<List<LeaveRequestStatusHistoryDto>>(leaveRequestStatusHistories);
    }
}