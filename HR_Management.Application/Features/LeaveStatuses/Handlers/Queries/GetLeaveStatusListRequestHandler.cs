using AutoMapper;
using HR_Management.Application.DTOs.LeaveStatus;
using HR_Management.Application.Features.LeaveStatus.Requests.Queries;
using HR_Management.Application.Persistence.Contracts;
using MediatR;

namespace HR_Management.Application.Features.LeaveStatuses.Handlers.Queries;

public class GetLeaveStatusListRequestHandler : IRequestHandler<GetLeaveStatusListRequest, List<LeaveStatusDto>>
{
    private readonly ILeaveStatusRepository _leaveStatusRepo;
    private readonly IMapper _mapper;

    public GetLeaveStatusListRequestHandler(ILeaveStatusRepository leaveStatusRepo, IMapper mapper)
    {
        _leaveStatusRepo = leaveStatusRepo;
        _mapper = mapper;
    }

    public async Task<List<LeaveStatusDto>> Handle(GetLeaveStatusListRequest request,
        CancellationToken cancellationToken)
    {
        var leaveStatuses = await _leaveStatusRepo.GetAll();
        return _mapper.Map<List<LeaveStatusDto>>(leaveStatuses);
    }
}