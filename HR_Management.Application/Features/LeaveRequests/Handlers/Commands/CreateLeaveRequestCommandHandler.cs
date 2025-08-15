using AutoMapper;
using HR_Management.Application.DTOs.LeaveRequest.Validators;
using HR_Management.Application.Features.LeaveRequests.Requests.Commands;
using HR_Management.Application.Persistence.Contracts;
using HR_Management.Domain;
using MediatR;

namespace HR_Management.Application.Features.LeaveRequests.Handlers.Commands;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, int>
{
    private readonly ILeaveRequestRepository _leaveRequestRepo;
    private readonly ILeaveTypeRepository _leaveTypeRepo;
    private readonly IMapper _mapper;

    public CreateLeaveRequestCommandHandler(
        ILeaveRequestRepository leaveRequestRepo,
        IMapper mapper,
        ILeaveTypeRepository leaveTypeRepo)
    {
        _leaveRequestRepo = leaveRequestRepo;
        _mapper = mapper;
        _leaveTypeRepo = leaveTypeRepo;
    }

    public async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveRequestDtoValidator(_leaveTypeRepo);
        var validationResult = await validator.ValidateAsync(request.CreateLeaveRequestDto);

        if (!validationResult.IsValid) throw new Exception("Validation failed");

        var leaveRequest = _mapper.Map<LeaveRequest>(request.CreateLeaveRequestDto);
        leaveRequest = await _leaveRequestRepo.Add(leaveRequest);
        return leaveRequest.Id;
    }
}