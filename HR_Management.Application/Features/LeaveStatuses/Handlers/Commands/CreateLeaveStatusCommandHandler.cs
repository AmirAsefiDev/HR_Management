using AutoMapper;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveStatus.Validators;
using HR_Management.Application.Features.LeaveStatuses.Requests.Commands;
using HR_Management.Common;
using HR_Management.Domain;
using MediatR;

namespace HR_Management.Application.Features.LeaveStatuses.Handlers.Commands;

public class CreateLeaveStatusCommandHandler : IRequestHandler<CreateLeaveStatusCommand, ResultDto<int>>
{
    // Assuming you have a repository for LeaveStatus
    private readonly ILeaveStatusRepository _leaveStatusRepository;
    private readonly IMapper _mapper;

    public CreateLeaveStatusCommandHandler(ILeaveStatusRepository leaveStatusRepository, IMapper mapper)
    {
        _leaveStatusRepository = leaveStatusRepository;
        _mapper = mapper;
    }

    public async Task<ResultDto<int>> Handle(CreateLeaveStatusCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveStatusDtoValidator();
        var validationResult = await validator.ValidateAsync(request.CreateLeaveStatusDto, cancellationToken);
        if (!validationResult.IsValid)
            return ResultDto<int>.Failure(validationResult.Errors.First().ErrorMessage);

        var leaveStatus = _mapper.Map<LeaveStatus>(request.CreateLeaveStatusDto);
        leaveStatus = await _leaveStatusRepository.AddAsync(leaveStatus);

        // Assuming Id is set after adding to the repository
        return ResultDto<int>.Success(leaveStatus.Id, "Creation leaveStatus was successful", 201);
    }
}