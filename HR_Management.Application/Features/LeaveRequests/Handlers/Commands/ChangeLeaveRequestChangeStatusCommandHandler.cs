using ERP.Application.Interfaces.Email;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveRequest.Validators;
using HR_Management.Application.Features.LeaveRequests.Requests.Commands;
using HR_Management.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HR_Management.Application.Features.LeaveRequests.Handlers.Commands;

public class
    ChangeLeaveRequestChangeStatusCommandHandler : IRequestHandler<ChangeLeaveRequestChangeStatusCommand, ResultDto>
{
    private readonly IEmailService _emailService;
    private readonly ILeaveRequestRepository _leaveRequestRepo;
    private readonly ILeaveRequestStatusHistoryRepository _leaveRequestStatusHistoryRepo;
    private readonly ILogger<ChangeLeaveRequestChangeStatusCommandHandler> _logger;

    public ChangeLeaveRequestChangeStatusCommandHandler(
        ILeaveRequestRepository leaveRequestRepo,
        IEmailService emailService,
        ILogger<ChangeLeaveRequestChangeStatusCommandHandler> logger,
        ILeaveRequestStatusHistoryRepository leaveRequestStatusHistoryRepo)
    {
        _leaveRequestRepo = leaveRequestRepo;
        _emailService = emailService;
        _logger = logger;
        _leaveRequestStatusHistoryRepo = leaveRequestStatusHistoryRepo;
    }

    public async Task<ResultDto> Handle(ChangeLeaveRequestChangeStatusCommand request,
        CancellationToken cancellationToken)
    {
        var validator = new ChangeLeaveRequestApprovalValidator();
        var validationResult =
            await validator.ValidateAsync(request.ChangeLeaveRequestChangeStatusDto, cancellationToken);

        if (!validationResult.IsValid)
            return ResultDto.Failure(validationResult.Errors.First().ErrorMessage);

        var leaveRequest = await _leaveRequestRepo.Get(request.ChangeLeaveRequestChangeStatusDto.Id);
        if (leaveRequest == null)
            return ResultDto.Failure(
                $"No leave request found with Id = {request.ChangeLeaveRequestChangeStatusDto.Id}.");

        await _leaveRequestRepo.ChangeApprovalStatus(leaveRequest,
            request.ChangeLeaveRequestChangeStatusDto.approvalStatus);

        await _leaveRequestStatusHistoryRepo.Add(new Domain.LeaveRequestStatusHistory
        {
            ChangedBy = request.UserId,
            CreatedBy = leaveRequest.User.FullName,
            LeaveRequestId = request.ChangeLeaveRequestChangeStatusDto.Id,
            LeaveStatusId = (int)request.ChangeLeaveRequestChangeStatusDto.approvalStatus,
            Comment = request.ChangeLeaveRequestChangeStatusDto.Comment
        });

        var statusText = request.ChangeLeaveRequestChangeStatusDto.approvalStatus switch
        {
            ILeaveRequestRepository.ApprovalStatuses.Pending => "Pending ⏳",
            ILeaveRequestRepository.ApprovalStatuses.Approved => "Approved ✅",
            ILeaveRequestRepository.ApprovalStatuses.Rejected => "Rejected ❌",
            ILeaveRequestRepository.ApprovalStatuses.Cancelled => "Cancelled 📴",
            _ => request.ChangeLeaveRequestChangeStatusDto.approvalStatus.ToString()
        };

        try
        {
            await _emailService.SendEmail(new EmailDto
            {
                Destination = leaveRequest.User.Email,
                Title = "Your Leave Request Status Has Been Updated",
                MessageBody =
                    $@"<p>Hello {leaveRequest.User.FullName},</p>

                    <p>Your leave request with ID <b>{leaveRequest.Id}</b> has been reviewed.<br/>
                    The current status of your request is:</p>

                    <p>📌 <b>Status:</b> {statusText}</p>

                    <p>Thank you for your patience and understanding.<br/>
                    If you have any questions, please contact the HR department.</p>

                    <p>Best regards,<br/>
                    HR Management Team</p>"
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "an error happened while ending email to change leaveRequest status.");
            throw new Exception(e.Message, e);
        }


        return ResultDto.Success("Status of LeaveRequest Changed Successfully.");
    }
}