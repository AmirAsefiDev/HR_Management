using FluentValidation;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveRequest.CreateLeaveRequest;

namespace HR_Management.Application.DTOs.LeaveRequest.Validators;

public class CreateLeaveRequestDtoValidator : AbstractValidator<CreateLeaveRequestDto>
{
    private readonly ILeaveStatusRepository _leaveStatusRepo;
    private readonly ILeaveTypeRepository _leaveTypeRepo;

    public CreateLeaveRequestDtoValidator(ILeaveTypeRepository leaveTypeRepo, ILeaveStatusRepository leaveStatusRepo)
    {
        _leaveTypeRepo = leaveTypeRepo;
        _leaveStatusRepo = leaveStatusRepo;
        Include(new ILeaveRequestDtoValidator(_leaveTypeRepo, _leaveStatusRepo));
    }
}