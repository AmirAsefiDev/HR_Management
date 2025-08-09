using AutoMapper;
using HR_Management.Application.Features.LeaveRequests.Requests.Commands;
using HR_Management.Application.Persistence.Contracts;
using HR_Management.Domain;
using MediatR;

namespace HR_Management.Application.Features.LeaveRequests.Handlers.Commands;

public class LeaveRequestCommandHandler : IRequestHandler<LeaveRequestCommand, int>
{
    private readonly ILeaveRequestRepository _leaveRequestRepo;
    private readonly IMapper _mapper;

    public LeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepo, IMapper mapper)
    {
        _leaveRequestRepo = leaveRequestRepo;
        _mapper = mapper;
    }


    public async Task<int> Handle(LeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = _mapper.Map<LeaveRequest>(request.LeaveRequestDto);
        leaveRequest = await _leaveRequestRepo.Add(leaveRequest);
        return leaveRequest.Id;
    }
}