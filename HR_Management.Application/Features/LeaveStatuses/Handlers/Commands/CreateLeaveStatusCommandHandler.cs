using AutoMapper;
using HR_Management.Application.DTOs.LeaveStatus.Validators;
using HR_Management.Application.Features.LeaveStatuses.Requests.Commands;
using HR_Management.Application.Persistence.Contracts;
using HR_Management.Application.Responses;
using HR_Management.Domain;
using MediatR;

namespace HR_Management.Application.Features.LeaveStatuses.Handlers.Commands;

public class CreateLeaveStatusCommandHandler : IRequestHandler<CreateLeaveStatusCommand, BaseCommandResponse>
{
    // Assuming you have a repository for LeaveStatus
    private readonly ILeaveStatusRepository _leaveStatusRepository;
    private readonly IMapper _mapper;

    public CreateLeaveStatusCommandHandler(ILeaveStatusRepository leaveStatusRepository, IMapper mapper)
    {
        _leaveStatusRepository = leaveStatusRepository;
        _mapper = mapper;
    }

    public async Task<BaseCommandResponse> Handle(CreateLeaveStatusCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();

        var validator = new CreateLeaveStatusDtoValidator();
        var validationResult = await validator.ValidateAsync(request.CreateLeaveStatusDto);
        if (!validationResult.IsValid)
        {
            response.Success = false;
            response.Message = "Creation Failed";
            response.Errors = validationResult.Errors
                .Select(e => e.ErrorMessage)
                .ToList();
        }

        var leaveStatus = _mapper.Map<LeaveStatus>(request.CreateLeaveStatusDto);
        leaveStatus = await _leaveStatusRepository.Add(leaveStatus);

        response.Id = leaveStatus.Id; // Assuming Id is set after adding to the repository
        response.Success = true;
        response.Message = "Creation LeaveStatus successful";
        return response;
    }
}