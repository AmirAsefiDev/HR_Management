using FluentValidation;
using HR_Management.Application.Contracts.Persistence;

namespace HR_Management.Application.DTOs.LeaveAllocation.Validators;

public class CreateLeaveAllocationDtoValidator : AbstractValidator<CreateLeaveAllocationDto>
{
    private readonly ILeaveTypeRepository _leaveTypeRepo;

    public CreateLeaveAllocationDtoValidator(ILeaveTypeRepository leaveTypeRepo)
    {
        _leaveTypeRepo = leaveTypeRepo;
        Include(new ILeaveAllocationDtoValidator(_leaveTypeRepo));
    }
}