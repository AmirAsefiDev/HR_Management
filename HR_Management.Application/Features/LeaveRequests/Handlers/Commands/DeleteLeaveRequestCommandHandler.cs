using HR_Management.Application.Features.LeaveRequests.Requests.Commands;
using HR_Management.Application.Persistence.Contracts;
using MediatR;

namespace HR_Management.Application.Features.LeaveRequests.Handlers.Commands;

public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepo;

    public DeleteLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepo)
    {
        _leaveRequestRepo = leaveRequestRepo;
    }

    public async Task<Unit> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepo.Get(request.Id);
        await _leaveRequestRepo.Delete(leaveRequest);
        return Unit.Value;
    }
}