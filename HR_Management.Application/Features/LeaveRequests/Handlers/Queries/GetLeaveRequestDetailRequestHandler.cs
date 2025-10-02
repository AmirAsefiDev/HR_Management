using AutoMapper;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveRequest;
using HR_Management.Application.Features.LeaveRequests.Requests.Queries;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.LeaveRequests.Handlers.Queries;

public class
    GetLeaveRequestDetailRequestHandler : IRequestHandler<GetLeaveRequestDetailRequest, ResultDto<LeaveRequestDto>>
{
    private readonly ILeaveRequestRepository _leaveRequestRepo;
    private readonly IMapper _mapper;

    public GetLeaveRequestDetailRequestHandler(ILeaveRequestRepository leaveRequestRepo, IMapper mapper)
    {
        _leaveRequestRepo = leaveRequestRepo;
        _mapper = mapper;
    }

    public async Task<ResultDto<LeaveRequestDto>> Handle(GetLeaveRequestDetailRequest request,
        CancellationToken cancellationToken)
    {
        var getLeaveRequest = await _leaveRequestRepo.GetLeaveRequestWithDetails(request.Id);

        if (getLeaveRequest == null)
            return ResultDto<LeaveRequestDto>.Failure("No leave request found.", 404);

        return ResultDto<LeaveRequestDto>.Success(_mapper.Map<LeaveRequestDto>(getLeaveRequest));
    }
}