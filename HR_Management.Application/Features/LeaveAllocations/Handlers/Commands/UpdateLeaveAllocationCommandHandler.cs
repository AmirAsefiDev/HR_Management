using AutoMapper;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveAllocation.Validators;
using HR_Management.Application.Exceptions;
using HR_Management.Application.Features.LeaveAllocations.Requests.Commands;
using HR_Management.Domain;
using MediatR;

namespace HR_Management.Application.Features.LeaveAllocations.Handlers.Commands;

public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepo;
    private readonly ILeaveTypeRepository _leaveTypeRepo;
    private readonly IMapper _mapper;

    public UpdateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepo,
        IMapper mapper,
        ILeaveTypeRepository leaveTypeRepo)
    {
        _leaveAllocationRepo = leaveAllocationRepo;
        _mapper = mapper;
        _leaveTypeRepo = leaveTypeRepo;
    }

    public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveAllocationDtoValidator(_leaveTypeRepo);
        var validationResult = await validator.ValidateAsync(request.UpdateLeaveAllocationDto);
        if (!validationResult.IsValid) throw new ValidationException(validationResult);

        var leaveAllocation = await _leaveAllocationRepo.Get(request.UpdateLeaveAllocationDto.Id);
        _mapper.Map<LeaveAllocation>(request.UpdateLeaveAllocationDto);
        await _leaveAllocationRepo.Update(leaveAllocation);
        return Unit.Value;
    }
}