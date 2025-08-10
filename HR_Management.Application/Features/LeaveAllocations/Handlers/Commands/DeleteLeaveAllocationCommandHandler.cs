using HR_Management.Application.Features.LeaveAllocations.Requests.Commands;
using HR_Management.Application.Persistence.Contracts;
using MediatR;

namespace HR_Management.Application.Features.LeaveAllocations.Handlers.Commands;

public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand, Unit>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepo;

    public DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepo)
    {
        _leaveAllocationRepo = leaveAllocationRepo;
    }

    public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var leaveAllocation = await _leaveAllocationRepo.Get(request.Id);
        await _leaveAllocationRepo.Delete(leaveAllocation);
        return Unit.Value;
    }
}