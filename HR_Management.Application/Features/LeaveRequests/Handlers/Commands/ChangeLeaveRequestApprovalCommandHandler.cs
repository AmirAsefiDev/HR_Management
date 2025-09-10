using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveRequest.Validators;
using HR_Management.Application.Features.LeaveRequests.Requests.Commands;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.LeaveRequests.Handlers.Commands;

public class ChangeLeaveRequestApprovalCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand, ResultDto>
{
    private readonly ILeaveRequestRepository _leaveRequestRepo;

    public ChangeLeaveRequestApprovalCommandHandler(ILeaveRequestRepository leaveRequestRepo)
    {
        _leaveRequestRepo = leaveRequestRepo;
    }

    public async Task<ResultDto> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
    {
        var validator = new ChangeLeaveRequestApprovalValidator();
        var validationResult = await validator.ValidateAsync(request.ChangeLeaveRequestApprovalDto, cancellationToken);

        if (!validationResult.IsValid)
            return ResultDto.Failure(validationResult.Errors.First().ErrorMessage);

        var leaveRequest = await _leaveRequestRepo.Get(request.ChangeLeaveRequestApprovalDto.Id);
        if (leaveRequest == null)
            return ResultDto.Failure($"No leave request found with Id = {request.ChangeLeaveRequestApprovalDto.Id}.");

        await _leaveRequestRepo.ChangeApprovalStatus(leaveRequest,
            request.ChangeLeaveRequestApprovalDto.approvalStatus);

        return ResultDto.Success("Status of LeaveRequest Changed Successfully.");
    }
}