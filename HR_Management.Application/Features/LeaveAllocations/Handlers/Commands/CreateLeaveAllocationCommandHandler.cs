using AutoMapper;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveAllocation.Validators;
using HR_Management.Application.Features.LeaveAllocations.Requests.Commands;
using HR_Management.Common;
using HR_Management.Domain;
using MediatR;

namespace HR_Management.Application.Features.LeaveAllocations.Handlers.Commands;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, ResultDto<int>>
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

    public async Task<ResultDto<int>> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveAllocationDtoValidator(_leaveTypeRepo);
        var validationResult = await validator.ValidateAsync(request.CreateLeaveAllocationDto, cancellationToken);

        if (!validationResult.IsValid) return ResultDto<int>.Failure(validationResult.Errors.First().ErrorMessage);

        // var leaveAllocation = _mapper.Map<LeaveAllocation>(request.CreateLeaveAllocationDto);
        // leaveAllocation = await _leaveAllocationRepo.Add(leaveAllocation);

        var leaveType = await _leaveTypeRepo.GetAsync(request.CreateLeaveAllocationDto.LeaveTypeId);
        var leaveAllocation = new LeaveAllocation
        {
            LeaveTypeId = leaveType.Id,
            UserId = request.CreateLeaveAllocationDto.UserId,
            Period = DateTime.UtcNow.Year,
            TotalDays = leaveType.DefaultDay
        };

        await _leaveAllocationRepo.AddAsync(leaveAllocation);
        return ResultDto<int>.Success(leaveAllocation.Id, "Leave Allocation Created Successfully.", 201);
    }
}