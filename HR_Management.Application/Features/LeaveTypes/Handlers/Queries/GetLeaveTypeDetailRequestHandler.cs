using AutoMapper;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveType;
using HR_Management.Application.Features.LeaveTypes.Requests.Queries;
using MediatR;

namespace HR_Management.Application.Features.LeaveTypes.Handlers.Queries;

public class GetLeaveTypeDetailRequestHandler : IRequestHandler<GetLeaveTypeDetailRequest, LeaveTypeDto>
{
    private readonly ILeaveTypeRepository _leaveTypeRepo;
    private readonly IMapper _mapper;

    public GetLeaveTypeDetailRequestHandler(ILeaveTypeRepository leaveTypeRepo, IMapper mapper)
    {
        _leaveTypeRepo = leaveTypeRepo;
        _mapper = mapper;
    }

    public async Task<LeaveTypeDto> Handle(GetLeaveTypeDetailRequest request, CancellationToken cancellationToken)
    {
        var getLeaveType = await _leaveTypeRepo.Get(request.Id);
        return _mapper.Map<LeaveTypeDto>(getLeaveType);
    }
}