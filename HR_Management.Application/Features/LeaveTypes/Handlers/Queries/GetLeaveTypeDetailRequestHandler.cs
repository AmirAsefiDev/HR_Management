using AutoMapper;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveType;
using HR_Management.Application.Features.LeaveTypes.Requests.Queries;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.LeaveTypes.Handlers.Queries;

public class GetLeaveTypeDetailRequestHandler : IRequestHandler<GetLeaveTypeDetailRequest, ResultDto<LeaveTypeDto>>
{
    private readonly ILeaveTypeRepository _leaveTypeRepo;
    private readonly IMapper _mapper;

    public GetLeaveTypeDetailRequestHandler(ILeaveTypeRepository leaveTypeRepo, IMapper mapper)
    {
        _leaveTypeRepo = leaveTypeRepo;
        _mapper = mapper;
    }

    public async Task<ResultDto<LeaveTypeDto>> Handle(GetLeaveTypeDetailRequest request,
        CancellationToken cancellationToken)
    {
        var getLeaveType = await _leaveTypeRepo.GetAsync(request.Id);
        if (getLeaveType == null)
            return ResultDto<LeaveTypeDto>.Failure($"No leaveType found with {request.Id}.", 404);

        var dto = _mapper.Map<LeaveTypeDto>(getLeaveType);
        return ResultDto<LeaveTypeDto>.Success(dto);
    }
}