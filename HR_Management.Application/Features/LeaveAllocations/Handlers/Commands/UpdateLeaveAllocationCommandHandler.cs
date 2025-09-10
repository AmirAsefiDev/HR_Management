using AutoMapper;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveAllocation.Validators;
using HR_Management.Application.Features.LeaveAllocations.Requests.Commands;
using HR_Management.Common;
using HR_Management.Domain;
using MediatR;

namespace HR_Management.Application.Features.LeaveAllocations.Handlers.Commands;

public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, ResultDto>
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

    public async Task<ResultDto> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveAllocationDtoValidator(_leaveTypeRepo);
        var validationResult = await validator.ValidateAsync(request.UpdateLeaveAllocationDto, cancellationToken);
        if (!validationResult.IsValid) return ResultDto.Failure(validationResult.Errors.First().ErrorMessage);

        var leaveAllocation = await _leaveAllocationRepo.Get(request.UpdateLeaveAllocationDto.Id);

        if (leaveAllocation == null)
            ResultDto.Failure($"No leave allocation found with Id = {request.UpdateLeaveAllocationDto.Id}.");

        _mapper.Map<LeaveAllocation>(request.UpdateLeaveAllocationDto);
        await _leaveAllocationRepo.Update(leaveAllocation);

        return ResultDto.Success("Leave allocation Updated Successfully.");
    }
}