using AutoMapper;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveStatus;
using HR_Management.Application.Features.LeaveStatuses.Requests.Queries;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.LeaveStatuses.Handlers.Queries;

public class
    GetLeaveStatusDetailRequestHandler : IRequestHandler<GetLeaveStatusDetailRequest, ResultDto<LeaveStatusDto>>
{
    private readonly ILeaveStatusRepository _leaveStatusRepo;
    private readonly IMapper _mapper;

    public GetLeaveStatusDetailRequestHandler(ILeaveStatusRepository leaveStatusRepo, IMapper mapper)
    {
        _leaveStatusRepo = leaveStatusRepo;
        _mapper = mapper;
    }

    public async Task<ResultDto<LeaveStatusDto>> Handle(GetLeaveStatusDetailRequest request,
        CancellationToken cancellationToken)
    {
        if (request.Id <= 0)
            return ResultDto<LeaveStatusDto>.Failure("Enter valid leave status Id.");

        var getLeaveStatus = await _leaveStatusRepo.GetAsync(request.Id);

        if (getLeaveStatus == null)
            return ResultDto<LeaveStatusDto>.Failure("No leave status found.", 404);

        return ResultDto<LeaveStatusDto>.Success(_mapper.Map<LeaveStatusDto>(getLeaveStatus));
    }
}