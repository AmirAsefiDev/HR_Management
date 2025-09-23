using FluentValidation;
using HR_Management.Application.DTOs.LeaveRequest.ChangeLeaveRequestApproval;

namespace HR_Management.Application.DTOs.LeaveRequest.Validators;

public class ChangeLeaveRequestApprovalValidator : AbstractValidator<ChangeLeaveRequestChangeStatusDto>
{
    public ChangeLeaveRequestApprovalValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

        RuleFor(c => c.approvalStatus)
            .IsInEnum().WithMessage("{PropertyName} must be a valid approval status");
        RuleFor(c => c.Comment)
            .NotEmpty().WithMessage("Please Enter {PropertyName}.");
    }
}