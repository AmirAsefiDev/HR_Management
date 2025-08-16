using AutoMapper;
using HR_Management.Application.DTOs.LeaveStatus.Validators;
using HR_Management.Application.Exceptions;
using HR_Management.Application.Features.LeaveStatuses.Requests.Commands;
using HR_Management.Application.Persistence.Contracts;
using HR_Management.Domain;
using MediatR;

namespace HR_Management.Application.Features.LeaveStatuses.Handlers.Commands;

public class UpdateLeaveStatusCommandHandler
{
    private readonly ILeaveStatusRepository _leaveStatusRepo;
    private readonly IMapper _mapper;

    public UpdateLeaveStatusCommandHandler(ILeaveStatusRepository leaveStatusRepo, IMapper mapper)
    {
        _leaveStatusRepo = leaveStatusRepo;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateLeaveStatusCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveStatusDtoValidator();
        var validationResult = await validator.ValidateAsync(request.UpdateLeaveStatusDto);

        if (!validationResult.IsValid) throw new ValidationException(validationResult);

        var leaveStatus = await _leaveStatusRepo.Get(request.UpdateLeaveStatusDto.Id);
        if (leaveStatus == null)
            throw new NotFoundException(nameof(LeaveStatus), request.UpdateLeaveStatusDto.Id);

        _mapper.Map(request.UpdateLeaveStatusDto, leaveStatus);
        await _leaveStatusRepo.Update(leaveStatus);

        return Unit.Value;
    }
}