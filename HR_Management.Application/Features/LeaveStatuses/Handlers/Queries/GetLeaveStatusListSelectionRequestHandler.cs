using AutoMapper;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveStatus;
using HR_Management.Application.Features.LeaveStatuses.Requests.Queries;
using MediatR;

namespace HR_Management.Application.Features.LeaveStatuses.Handlers.Queries;

public class
    GetLeaveStatusListSelectionRequestHandler : IRequestHandler<GetLeaveStatusListSelectionRequest,
    List<LeaveStatusDto>>
{
    private readonly ILeaveStatusRepository _leaveStatusRepo;
    private readonly IMapper _mapper;

    public GetLeaveStatusListSelectionRequestHandler(ILeaveStatusRepository leaveStatusRepo, IMapper mapper)
    {
        _leaveStatusRepo = leaveStatusRepo;
        _mapper = mapper;
    }

    public async Task<List<LeaveStatusDto>> Handle(GetLeaveStatusListSelectionRequest request,
        CancellationToken cancellationToken)
    {
        var leaveStatuses = await _leaveStatusRepo.GetAllAsync();
        return _mapper.Map<List<LeaveStatusDto>>(leaveStatuses);
    }
}