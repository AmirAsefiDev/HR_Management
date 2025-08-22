using AutoMapper;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.Features.LeaveAllocations.Requests.Commands;
using HR_Management.Domain;
using MediatR;

namespace HR_Management.Application.Features.LeaveAllocations.Handlers.Commands;

public class LeaveAllocationCommandHandler : IRequestHandler<LeaveAllocationCommand, int>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepo;
    private readonly IMapper _mapper;

    public LeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepo, IMapper mapper)
    {
        _leaveAllocationRepo = leaveAllocationRepo;
        _mapper = mapper;
    }

    public async Task<int> Handle(LeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var leaveAllocation = _mapper.Map<LeaveAllocation>(request.LeaveAllocationDto);
        leaveAllocation = await _leaveAllocationRepo.Add(leaveAllocation);
        return leaveAllocation.Id;
    }
}