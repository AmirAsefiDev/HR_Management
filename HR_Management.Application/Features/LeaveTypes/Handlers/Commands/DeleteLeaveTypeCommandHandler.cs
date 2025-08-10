using HR_Management.Application.Features.LeaveTypes.Requests.Commands;
using HR_Management.Application.Persistence.Contracts;
using MediatR;

namespace HR_Management.Application.Features.LeaveTypes.Handlers.Commands;

public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
{
    private readonly ILeaveTypeRepository _leaveTypeRepo;

    public DeleteLeaveTypeCommandHandler(
        ILeaveTypeRepository leaveTypeRepo)
    {
        _leaveTypeRepo = leaveTypeRepo;
    }

    public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var leaveType = await _leaveTypeRepo.Get(request.Id);
        await _leaveTypeRepo.Delete(leaveType);
        return Unit.Value;
    }
}