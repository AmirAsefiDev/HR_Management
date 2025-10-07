using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.Features.LeaveTypes.Requests.Commands;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.LeaveTypes.Handlers.Commands;

public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, ResultDto>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepo;
    private readonly ILeaveRequestRepository _leaveRequestRepo;
    private readonly ILeaveTypeRepository _leaveTypeRepo;

    public DeleteLeaveTypeCommandHandler(
        ILeaveTypeRepository leaveTypeRepo,
        ILeaveRequestRepository leaveRequestRepo,
        ILeaveAllocationRepository leaveAllocationRepo
    )
    {
        _leaveTypeRepo = leaveTypeRepo;
        _leaveRequestRepo = leaveRequestRepo;
        _leaveAllocationRepo = leaveAllocationRepo;
    }

    public async Task<ResultDto> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        if (request.Id == 0)
            return ResultDto.Failure("Please enter leave type Id.");
        if (request.Id < 0)
            return ResultDto.Failure("Please enter leave type id correctly.");

        var leaveType = await _leaveTypeRepo.GetAsync(request.Id);
        if (leaveType == null) return ResultDto.Failure("The requested leave type was not found.");

        var hasAnyLeaveRequest = await _leaveRequestRepo.HasAnyLeaveRequestWithTypeIdAsync(request.Id);
        var hasAnyLeaveAllocation = await _leaveAllocationRepo.HasAnyLeaveAllocationWithTypeIdAsync(request.Id);

        if (hasAnyLeaveRequest || hasAnyLeaveAllocation)
            return ResultDto.Failure(
                "This leave type is used by one leave request or leave allocation and can't be deleted.",
                409);

        await _leaveTypeRepo.DeleteAsync(leaveType);
        return ResultDto.Success("The requested leave type has been successfully deleted.");
    }
}