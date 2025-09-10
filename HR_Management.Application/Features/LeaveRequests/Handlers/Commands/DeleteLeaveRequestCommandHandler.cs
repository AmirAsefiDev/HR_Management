using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.Features.LeaveRequests.Requests.Commands;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.LeaveRequests.Handlers.Commands;

public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand, ResultDto>
{
    private readonly ILeaveRequestRepository _leaveRequestRepo;

    public DeleteLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepo)
    {
        _leaveRequestRepo = leaveRequestRepo;
    }

    public async Task<ResultDto> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        if (request.Id <= 0)
            return ResultDto.Failure("Enter LeaveRequestId Correctly.");

        var leaveRequest = await _leaveRequestRepo.Get(request.Id);
        if (leaveRequest == null)
            return ResultDto.Failure($"LeaveRequest with Id ={request.Id},didn't find.");

        await _leaveRequestRepo.Delete(leaveRequest);
        return ResultDto.Success("LeaveRequest deleted successfully");
    }
}