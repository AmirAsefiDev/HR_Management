using AutoMapper;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveStatus.Validators;
using HR_Management.Application.Features.LeaveStatuses.Requests.Commands;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.LeaveStatuses.Handlers.Commands;

public class UpdateLeaveStatusCommandHandler : IRequestHandler<UpdateLeaveStatusCommand, ResultDto>
{
    private readonly ILeaveStatusRepository _leaveStatusRepo;
    private readonly IMapper _mapper;

    public UpdateLeaveStatusCommandHandler(ILeaveStatusRepository leaveStatusRepo, IMapper mapper)
    {
        _leaveStatusRepo = leaveStatusRepo;
        _mapper = mapper;
    }

    public async Task<ResultDto> Handle(UpdateLeaveStatusCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveStatusDtoValidator();
        var validationResult = await validator.ValidateAsync(request.UpdateLeaveStatusDto, cancellationToken);

        if (!validationResult.IsValid)
            return ResultDto.Failure(validationResult.Errors.First().ErrorMessage);

        var leaveStatus = await _leaveStatusRepo.GetAsync(request.UpdateLeaveStatusDto.Id);
        if (leaveStatus == null)
            return ResultDto.Failure("The leave status not found to edit.");

        _mapper.Map(request.UpdateLeaveStatusDto, leaveStatus);
        await _leaveStatusRepo.UpdateAsync(leaveStatus);

        return ResultDto.Success("Leave status successfully updated.");
    }
}