using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.Features.LeaveStatuses.Requests.Commands;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.LeaveStatuses.Handlers.Commands;

public class DeleteLeaveStatusCommandHandler : IRequestHandler<DeleteLeaveStatusCommand, ResultDto>
{
    private readonly ILeaveRequestRepository _leaveRequestRepo;
    private readonly ILeaveRequestStatusHistoryRepository _leaveRequestStatusHistoryRepo;
    private readonly ILeaveStatusRepository _leaveStatusRepo;

    public DeleteLeaveStatusCommandHandler(ILeaveStatusRepository leaveStatusRepo,
        ILeaveRequestRepository leaveRequestRepo,
        ILeaveRequestStatusHistoryRepository leaveRequestStatusHistoryRepo)
    {
        _leaveStatusRepo = leaveStatusRepo;
        _leaveRequestRepo = leaveRequestRepo;
        _leaveRequestStatusHistoryRepo = leaveRequestStatusHistoryRepo;
    }

    public async Task<ResultDto> Handle(DeleteLeaveStatusCommand request, CancellationToken cancellationToken)
    {
        if (request.Id == 0)
            return ResultDto.Failure("Please enter leave status id.");
        if (request.Id < 0)
            return ResultDto.Failure("Please enter leave status id correctly.");

        var leaveStatus = await _leaveStatusRepo.GetAsync(request.Id);
        if (leaveStatus == null)
            return ResultDto.Failure($"No leave status found with Id={request.Id}");

        var hasAnyLeaveRequest = await _leaveRequestRepo
            .HasAnyLeaveRequestWithStatusIdAsync(leaveStatus.Id);
        var hasAnyLeaveHistory = await _leaveRequestStatusHistoryRepo
            .HasAnyLeaveHistoryWithStatusIdAsync(leaveStatus.Id);

        if (hasAnyLeaveRequest || hasAnyLeaveHistory)
            return ResultDto.Failure(
                "This leave status is used by one leave request or leave history and can't be deleted.", 409);

        await _leaveStatusRepo.DeleteAsync(leaveStatus);
        return ResultDto.Success("Leave status deleted successfully");
    }
}