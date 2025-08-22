using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.Exceptions;
using HR_Management.Application.Features.LeaveTypes.Requests.Commands;
using HR_Management.Domain;
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
        if (leaveType == null)
            throw new NotFoundException(nameof(LeaveType), request.Id);

        await _leaveTypeRepo.Delete(leaveType);
        return Unit.Value;
    }
}