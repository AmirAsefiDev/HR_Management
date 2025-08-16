using AutoMapper;
using HR_Management.Application.DTOs.LeaveStatus;
using HR_Management.Application.Features.LeaveStatuses.Requests.Queries;
using HR_Management.Application.Persistence.Contracts;
using MediatR;

namespace HR_Management.Application.Features.LeaveStatuses.Handlers.Queries;

public class GetLeaveStatusDetailRequestHandler : IRequestHandler<GetLeaveStatusDetailRequest, LeaveStatusDto>
{
    private readonly ILeaveStatusRepository _leaveStatusRepo;
    private readonly IMapper _mapper;

    public GetLeaveStatusDetailRequestHandler(ILeaveStatusRepository leaveStatusRepo, IMapper mapper)
    {
        _leaveStatusRepo = leaveStatusRepo;
        _mapper = mapper;
    }

    public async Task<LeaveStatusDto> Handle(GetLeaveStatusDetailRequest request, CancellationToken cancellationToken)
    {
        var getLeaveStatus = await _leaveStatusRepo.Get(request.Id);
        return _mapper.Map<LeaveStatusDto>(getLeaveStatus);
    }
}