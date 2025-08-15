using AutoMapper;
using HR_Management.Application.DTOs.LeaveAllocation.Validators;
using HR_Management.Application.Exceptions;
using HR_Management.Application.Features.LeaveAllocations.Requests.Commands;
using HR_Management.Application.Persistence.Contracts;
using HR_Management.Domain;
using MediatR;

namespace HR_Management.Application.Features.LeaveAllocations.Handlers.Commands;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, int>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepo;
    private readonly ILeaveTypeRepository _leaveTypeRepo;
    private readonly IMapper _mapper;

    public CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepo,
        IMapper mapper,
        ILeaveTypeRepository leaveTypeRepo)
    {
        _leaveAllocationRepo = leaveAllocationRepo;
        _mapper = mapper;
        _leaveTypeRepo = leaveTypeRepo;
    }

    public async Task<int> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveAllocationDtoValidator(_leaveTypeRepo);
        var validationResult = await validator.ValidateAsync(request.CreateLeaveAllocationDto);

        if (!validationResult.IsValid) throw new ValidationException(validationResult);

        var leaveAllocation = _mapper.Map<LeaveAllocation>(request.CreateLeaveAllocationDto);
        leaveAllocation = await _leaveAllocationRepo.Add(leaveAllocation);
        return leaveAllocation.Id;
    }
}