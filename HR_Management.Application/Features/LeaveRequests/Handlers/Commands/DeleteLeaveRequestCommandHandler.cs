using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.Features.LeaveRequests.Requests.Commands;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.LeaveRequests.Handlers.Commands;

public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand, ResultDto>
{
    private readonly ILeaveRequestRepository _leaveRequestRepo;
    private readonly ILeaveRequestStatusHistoryRepository _leaveRequestStatusHistoryRepo;

    public DeleteLeaveRequestCommandHandler(
        ILeaveRequestRepository leaveRequestRepo,
        ILeaveRequestStatusHistoryRepository leaveRequestStatusHistoryRepo
    )
    {
        _leaveRequestRepo = leaveRequestRepo;
        _leaveRequestStatusHistoryRepo = leaveRequestStatusHistoryRepo;
    }

    public async Task<ResultDto> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        if (request.Id == 0)
            return ResultDto.Failure("Please enter leave request id.");
        if (request.Id < 0)
            return ResultDto.Failure("Please enter leave request id correctly.");

        var leaveRequest = await _leaveRequestRepo.GetAsync(request.Id);
        if (leaveRequest == null)
            return ResultDto.Failure($"LeaveRequest with Id ={request.Id},didn't find.");

        var hasAnyLeaveHistory = await _leaveRequestStatusHistoryRepo
            .HasAnyLeaveHistoryWithRequestIdAsync(leaveRequest.Id);
        if (hasAnyLeaveHistory)
            return ResultDto.Failure("This leave request is used by one or more leave histories and can't be deleted.",
                409);

        await _leaveRequestRepo.DeleteAsync(leaveRequest);
        return ResultDto.Success("Leave request deleted successfully");
    }
}