using AutoMapper;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveType.Validators;
using HR_Management.Application.Features.LeaveTypes.Requests.Commands;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.LeaveTypes.Handlers.Commands;

public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, ResultDto>
{
    private readonly ILeaveTypeRepository _leaveTypeRepo;
    private readonly IMapper _mapper;

    public UpdateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepo, IMapper mapper)
    {
        _leaveTypeRepo = leaveTypeRepo;
        _mapper = mapper;
    }

    public async Task<ResultDto> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveTypeDtoValidator();
        var validationResult = await validator.ValidateAsync(request.LeaveTypeDto, cancellationToken);

        if (!validationResult.IsValid)
            return ResultDto.Failure(validationResult.Errors.First().ErrorMessage);

        var leaveType = await _leaveTypeRepo.GetAsync(request.LeaveTypeDto.Id);
        if (leaveType == null)
            return ResultDto.Failure($"No leave type was found by {request.LeaveTypeDto.Id}.");

        _mapper.Map(request.LeaveTypeDto, leaveType);

        await _leaveTypeRepo.UpdateAsync(leaveType);

        return ResultDto.Success("The requested leave type has been successfully updated.");
    }
}