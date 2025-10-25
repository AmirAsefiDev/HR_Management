using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.Features.LeaveAllocations.Requests.Commands;
using HR_Management.Common;
using HR_Management.Domain;
using MediatR;

namespace HR_Management.Application.Features.LeaveAllocations.Handlers.Commands;

public class RebuildLeaveAllocationsForNewYearCommandHandler
    : IRequestHandler<RebuildLeaveAllocationsForNewYearCommand, ResultDto>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepo;
    private readonly ILeaveTypeRepository _leaveTypeRepo;
    private readonly IUserRepository _userRepo;

    public RebuildLeaveAllocationsForNewYearCommandHandler(
        ILeaveAllocationRepository leaveAllocationRepo,
        IUserRepository userRepo,
        ILeaveTypeRepository leaveTypeRepo)
    {
        _leaveAllocationRepo = leaveAllocationRepo;
        _userRepo = userRepo;
        _leaveTypeRepo = leaveTypeRepo;
    }

    public async Task<ResultDto> Handle(RebuildLeaveAllocationsForNewYearCommand request,
        CancellationToken cancellationToken)
    {
        await _leaveAllocationRepo.DeleteAllAsync();


        var users = await _userRepo.GetAllAsync();
        var leaveTypes = await _leaveTypeRepo.GetAllAsync();

        var allocations = new List<LeaveAllocation>();
        foreach (var user in users)
        foreach (var leaveType in leaveTypes)
            allocations.Add(new LeaveAllocation
            {
                UserId = user.Id,
                LeaveTypeId = leaveType.Id,
                Period = DateTime.UtcNow.Year,
                TotalDays = leaveType.DefaultDay
            });

        await _leaveAllocationRepo.AddRangeAsync(allocations);

        return ResultDto.Success("Leave allocations has been reset for the new year.");
    }
}