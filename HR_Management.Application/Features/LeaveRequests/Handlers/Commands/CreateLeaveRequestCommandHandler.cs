using AutoMapper;
using ERP.Application.Interfaces.Email;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveRequest.Validators;
using HR_Management.Application.Features.LeaveRequests.Requests.Commands;
using HR_Management.Application.Responses;
using HR_Management.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HR_Management.Application.Features.LeaveRequests.Handlers.Commands;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, BaseCommandResponse>
{
    private readonly IEmailService _emailService;
    private readonly ILeaveRequestRepository _leaveRequestRepo;
    private readonly ILeaveStatusRepository _leaveStatusRepo;
    private readonly ILeaveTypeRepository _leaveTypeRepo;
    private readonly ILogger<CreateLeaveRequestCommandHandler> _logger;
    private readonly IMapper _mapper;

    public CreateLeaveRequestCommandHandler(
        ILeaveRequestRepository leaveRequestRepo,
        IMapper mapper,
        ILeaveTypeRepository leaveTypeRepo,
        ILeaveStatusRepository leaveStatusRepo,
        IEmailService emailService,
        ILogger<CreateLeaveRequestCommandHandler> logger)
    {
        _leaveRequestRepo = leaveRequestRepo;
        _mapper = mapper;
        _leaveTypeRepo = leaveTypeRepo;
        _leaveStatusRepo = leaveStatusRepo;
        _emailService = emailService;
        _logger = logger;
    }

    public async Task<BaseCommandResponse> Handle(CreateLeaveRequestCommand request,
        CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();

        var validator = new CreateLeaveRequestDtoValidator(_leaveTypeRepo, _leaveStatusRepo);
        var validationResult = await validator.ValidateAsync(request.CreateLeaveRequestDto);

        if (!validationResult.IsValid)
        {
            //throw new ValidationException(validationResult);
            response.Success = false;
            response.Message = "Creation failed";
            response.Errors = validationResult.Errors
                .Select(e => e.ErrorMessage)
                .ToList();
        }


        var leaveRequest = _mapper.Map<LeaveRequest>(request.CreateLeaveRequestDto);
        leaveRequest = await _leaveRequestRepo.Add(leaveRequest);

        response.Success = true;
        response.Message = "Creation successful";
        response.Id = leaveRequest.Id;

        var email = new EmailDto
        {
            Destination = "amirasefi.info@gmail.com",
            Title = "Leave Request Submitted",
            MessageBody =
                $"Your leave request for {request.CreateLeaveRequestDto.StartDate} \t\n " +
                $"to {request.CreateLeaveRequestDto.EndDate} has been submitted"
        };
        try
        {
            await _emailService.SendEmail(email);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "an error happened while ending email to create leaveRequest");
            throw new Exception(e.Message);
        }

        return response;
    }
}