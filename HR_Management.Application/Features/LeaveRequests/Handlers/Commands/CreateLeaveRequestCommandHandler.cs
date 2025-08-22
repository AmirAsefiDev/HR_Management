using AutoMapper;
using HR_Management.Application.Contracts.Infrastructure;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveRequest.Validators;
using HR_Management.Application.Features.LeaveRequests.Requests.Commands;
using HR_Management.Application.Models;
using HR_Management.Application.Responses;
using HR_Management.Domain;
using MediatR;

namespace HR_Management.Application.Features.LeaveRequests.Handlers.Commands;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, BaseCommandResponse>
{
    private readonly IEmailSender _emailSender;
    private readonly ILeaveRequestRepository _leaveRequestRepo;
    private readonly ILeaveStatusRepository _leaveStatusRepo;
    private readonly ILeaveTypeRepository _leaveTypeRepo;
    private readonly IMapper _mapper;

    public CreateLeaveRequestCommandHandler(
        ILeaveRequestRepository leaveRequestRepo,
        IMapper mapper,
        ILeaveTypeRepository leaveTypeRepo,
        ILeaveStatusRepository leaveStatusRepo,
        IEmailSender emailSender
    )
    {
        _leaveRequestRepo = leaveRequestRepo;
        _mapper = mapper;
        _leaveTypeRepo = leaveTypeRepo;
        _leaveStatusRepo = leaveStatusRepo;
        _emailSender = emailSender;
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

        var email = new Email
        {
            To = "amirasefi.info@gmail.com",
            Subject = "Leave Request Submitted",
            Body =
                $"Your leave request for {request.CreateLeaveRequestDto.StartDate} \t\n to {request.CreateLeaveRequestDto.EndDate} has been submitted"
        };
        try
        {
            await _emailSender.SendEmail(email);
        }
        catch (Exception e)
        {
            //log
        }

        return response;
    }
}