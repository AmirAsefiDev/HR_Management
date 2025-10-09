using AutoMapper;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveType;
using HR_Management.Application.Features.LeaveTypes.Requests.Queries;
using MediatR;

namespace HR_Management.Application.Features.LeaveTypes.Handlers.Queries;

public class
    GetLeaveTypeListSelectionRequestHandler : IRequestHandler<GetLeaveTypeListSelectionRequest, List<LeaveTypeDto>>
{
    private readonly ILeaveTypeRepository _leaveTypeRepo;
    private readonly IMapper _mapper;

    public GetLeaveTypeListSelectionRequestHandler(ILeaveTypeRepository leaveTypeRepo, IMapper mapper)
    {
        _leaveTypeRepo = leaveTypeRepo;
        _mapper = mapper;
    }

    public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypeListSelectionRequest request,
        CancellationToken cancellationToken)
    {
        var leaveTypes = await _leaveTypeRepo.GetAllAsync();

        return _mapper.Map<List<LeaveTypeDto>>(leaveTypes);
    }
}