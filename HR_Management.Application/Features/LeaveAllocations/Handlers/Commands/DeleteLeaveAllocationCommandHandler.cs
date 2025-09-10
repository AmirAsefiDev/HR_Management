using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveAllocation;
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
        if (request.Id <= 0)
            return ResultDto<LeaveAllocationDto>.Failure("Enter Leave Allocation Id Correctly.");

        var leaveAllocation = await _leaveAllocationRepo.Get(request.Id);
        if (leaveAllocation == null)
            ResultDto.Failure($"No leave allocation found with Id = {request.Id}.");

        await _leaveAllocationRepo.Delete(leaveAllocation);
        return ResultDto.Success("Leave allocation deleted successfully.");
    }
}