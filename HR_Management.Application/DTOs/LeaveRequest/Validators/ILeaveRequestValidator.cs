using FluentValidation;
using HR_Management.Application.Persistence.Contracts;

namespace HR_Management.Application.DTOs.LeaveRequest.Validators;

public class ILeaveRequestValidator : AbstractValidator<ILeaveRequestDto>
{
    private readonly ILeaveTypeRepository _leaveTypeRepo;

    public ILeaveRequestValidator(ILeaveTypeRepository leaveTypeRepo)
    {
        _leaveTypeRepo = leaveTypeRepo;
        RuleFor(l => l.StartDate)
            .LessThan(l => l.EndDate).WithMessage("{PropertyName} must be before {ComparisonValue}");
        RuleFor(l => l.EndDate)
            .GreaterThan(l => l.StartDate).WithMessage("{PropertyName} must be after {ComparisonValue}");
        RuleFor(l => l.LeaveTypeId)
            .GreaterThan(0)
            //optimized version
            .MustAsync(async (id, token) => !await _leaveTypeRepo.Exist(id))
            //simple version
            //.MustAsync(async (id, token) =>
            //{
            //    var leaveTypeExist = await _leaveRequestRepo.Exist(id);
            //    return !leaveTypeExist;
            //})
            .WithMessage("{PropertyName} doesn't exist");
    }
}