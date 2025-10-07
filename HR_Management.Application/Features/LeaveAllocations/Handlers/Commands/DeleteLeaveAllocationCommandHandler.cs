using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.Features.LeaveAllocations.Requests.Commands;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.LeaveAllocations.Handlers.Commands;

public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand, ResultDto>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepo;

    public DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepo)
    {
        _leaveAllocationRepo = leaveAllocationRepo;
    }

    public async Task<ResultDto> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        if (request.Id == 0)
            return ResultDto.Failure("Please enter leave allocation id.");
        if (request.Id < 0)
            return ResultDto.Failure("Please enter leave allocation id correctly.");

        var leaveAllocation = await _leaveAllocationRepo.GetAsync(request.Id);
        if (leaveAllocation == null)
            ResultDto.Failure($"No leave allocation found with Id = {request.Id}.");

        await _leaveAllocationRepo.DeleteAsync(leaveAllocation);
        return ResultDto.Success("Leave allocation deleted successfully.");
    }
}