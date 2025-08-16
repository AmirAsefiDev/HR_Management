using FluentValidation;

namespace HR_Management.Application.DTOs.LeaveStatus.Validators;

public class CreateLeaveStatusDtoValidator : AbstractValidator<CreateLeaveStatusDto>
{
    public CreateLeaveStatusDtoValidator()
    {
        Include(new ILeaveStatusDtoValidator());
    }
}