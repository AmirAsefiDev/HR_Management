using AutoMapper;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveAllocation;
using HR_Management.Application.Features.LeaveAllocations.Requests.Queries;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.LeaveAllocations.Handlers.Queries;

public class
    GetLeaveAllocationDetailRequestHandler : IRequestHandler<GetLeaveAllocationDetailRequest,
    ResultDto<LeaveAllocationDto>>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepo;
    private readonly IMapper _mapper;

    public GetLeaveAllocationDetailRequestHandler(ILeaveAllocationRepository LeaveAllocationRepo, IMapper mapper)
    {
        _leaveAllocationRepo = LeaveAllocationRepo;
        _mapper = mapper;
    }

    public async Task<ResultDto<LeaveAllocationDto>> Handle(GetLeaveAllocationDetailRequest request,
        CancellationToken cancellationToken)
    {
        if (request.Id <= 0)
            return ResultDto<LeaveAllocationDto>.Failure("Enter Valid Leave Allocation Id.");

        var getLeaveAllocation = await _leaveAllocationRepo.GetLeaveAllocationWithDetailsAsync(request.Id);

        if (getLeaveAllocation == null)
            return ResultDto<LeaveAllocationDto>.Failure($"No leave allocation with Id = {request.Id}.", 404);

        return ResultDto<LeaveAllocationDto>.Success(
            _mapper.Map<LeaveAllocationDto>(getLeaveAllocation)
            , "LeaveAllocation Retrieves Successfully.");
    }
}