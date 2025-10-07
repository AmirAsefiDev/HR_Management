using AutoMapper;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveType.Validators;
using HR_Management.Application.Features.LeaveTypes.Requests.Commands;
using HR_Management.Common;
using HR_Management.Domain;
using MediatR;

namespace HR_Management.Application.Features.LeaveTypes.Handlers.Commands;

public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, ResultDto<int>>
{
    private readonly ILeaveTypeRepository _leaveTypeRepo;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CreateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepo, IMapper mapper, IMediator mediator)
    {
        _leaveTypeRepo = leaveTypeRepo;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<ResultDto<int>> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        #region Validations

        var validator = new CreateLeaveTypeDtoValidator();
        var validationResult = await validator.ValidateAsync(request.LeaveTypeDto, cancellationToken);

        if (!validationResult.IsValid)
            return ResultDto<int>.Failure(validationResult.Errors.First().ErrorMessage);

        #endregion


        var leaveType = _mapper.Map<LeaveType>(request.LeaveTypeDto);
        leaveType = await _leaveTypeRepo.AddAsync(leaveType);

        //this handler add leave allocation to all to users which doesn't have leave allocation with this leave type.
        await _mediator.Publish(new LeaveTypeCreatedEvent(leaveType.Id, leaveType.DefaultDay), cancellationToken);

        return ResultDto<int>.Success(leaveType.Id, "The leave type has been successfully.", 201);
    }
}