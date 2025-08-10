using AutoMapper;
using HR_Management.Application.Features.LeaveRequests.Requests.Commands;
using HR_Management.Application.Persistence.Contracts;
using MediatR;

namespace HR_Management.Application.Features.LeaveRequests.Handlers.Commands;

public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepo;
    private readonly IMapper _mapper;

    public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepo, IMapper mapper)
    {
        _leaveRequestRepo = leaveRequestRepo;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepo.Get(request.Id);
        if (request.LeaveRequestDto != null)
        {
            _mapper.Map(request.LeaveRequestDto, leaveRequest);
            await _leaveRequestRepo.Update(leaveRequest);
            return Unit.Value;
        }

        if (request.ChangeLeaveRequestApprovalDto != null)
            await _leaveRequestRepo.ChangeApprovalStatus(leaveRequest, request.ChangeLeaveRequestApprovalDto.Aoorived);

        return Unit.Value;
    }
}