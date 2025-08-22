using AutoMapper;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveRequest.Validators;
using HR_Management.Application.Exceptions;
using HR_Management.Application.Features.LeaveRequests.Requests.Commands;
using MediatR;

namespace HR_Management.Application.Features.LeaveRequests.Handlers.Commands;

public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
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

    public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveRequestDtoValidator(_leaveTypeRepo, _leaveStatusRepo);
        var validationResult = await validator.ValidateAsync(request.UpdateLeaveRequestDto);

        if (!validationResult.IsValid) throw new ValidationException(validationResult);

        var leaveRequest = await _leaveRequestRepo.Get(request.Id);
        if (request.UpdateLeaveRequestDto != null)
        {
            _mapper.Map(request.UpdateLeaveRequestDto, leaveRequest);
            await _leaveRequestRepo.Update(leaveRequest);
            return Unit.Value;
        }

        if (request.ChangeLeaveRequestApprovalDto != null)
            await _leaveRequestRepo.ChangeApprovalStatus(leaveRequest,
                request.ChangeLeaveRequestApprovalDto.approvalStatus);

        return Unit.Value;
    }
}