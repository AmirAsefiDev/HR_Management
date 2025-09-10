using AutoMapper;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveRequest.Validators;
using HR_Management.Application.Features.LeaveRequests.Requests.Commands;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.LeaveRequests.Handlers.Commands;

public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, ResultDto>
{
    private readonly ILeaveRequestRepository _leaveRequestRepo;
    private readonly ILeaveStatusRepository _leaveStatusRepo;
    private readonly ILeaveTypeRepository _leaveTypeRepo;
    private readonly IMapper _mapper;

    public UpdateLeaveRequestCommandHandler(
        ILeaveRequestRepository leaveRequestRepo,
        IMapper mapper,
        ILeaveTypeRepository leaveTypeRepo,
        ILeaveStatusRepository leaveStatusRepo)
    {
        _leaveRequestRepo = leaveRequestRepo;
        _mapper = mapper;
        _leaveTypeRepo = leaveTypeRepo;
        _leaveStatusRepo = leaveStatusRepo;
    }

    public async Task<ResultDto> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveRequestDtoValidator(_leaveTypeRepo, _leaveStatusRepo);
        var validationResult = await validator.ValidateAsync(request.UpdateLeaveRequestDto, cancellationToken);

        if (!validationResult.IsValid) return ResultDto.Failure(validationResult.Errors.First().ErrorMessage);

        var leaveRequest = await _leaveRequestRepo.Get(request.Id);
        if (request.UpdateLeaveRequestDto == null)
            ResultDto.Failure($"No leave request found with Id = {request.Id}.");

        _mapper.Map(request.UpdateLeaveRequestDto, leaveRequest);
        await _leaveRequestRepo.Update(leaveRequest);
        return ResultDto.Success("LeaveRequest Updated Correctly.");
    }
}