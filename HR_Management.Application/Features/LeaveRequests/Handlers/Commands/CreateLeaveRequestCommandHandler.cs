using AutoMapper;
using ERP.Application.Interfaces.Email;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveRequest.Validators;
using HR_Management.Application.Features.LeaveRequests.Requests.Commands;
using HR_Management.Common;
using HR_Management.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HR_Management.Application.Features.LeaveRequests.Handlers.Commands;

public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, ResultDto<int>>
{
    private readonly IEmailService _emailService;
    private readonly ILeaveRequestRepository _leaveRequestRepo;
    private readonly ILeaveStatusRepository _leaveStatusRepo;
    private readonly ILeaveTypeRepository _leaveTypeRepo;
    private readonly ILogger<CreateLeaveRequestCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepo;

    public CreateLeaveRequestCommandHandler(
        ILeaveRequestRepository leaveRequestRepo,
        IMapper mapper,
        ILeaveTypeRepository leaveTypeRepo,
        ILeaveStatusRepository leaveStatusRepo,
        IEmailService emailService,
        ILogger<CreateLeaveRequestCommandHandler> logger,
        IUserRepository userRepo)
    {
        _leaveRequestRepo = leaveRequestRepo;
        _mapper = mapper;
        _leaveTypeRepo = leaveTypeRepo;
        _leaveStatusRepo = leaveStatusRepo;
        _emailService = emailService;
        _logger = logger;
        _userRepo = userRepo;
    }

    public async Task<ResultDto<int>> Handle(CreateLeaveRequestCommand request,
        CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveRequestDtoValidator(_leaveTypeRepo, _leaveStatusRepo);
        var validationResult = await validator.ValidateAsync(request.CreateLeaveRequestDto, cancellationToken);

        if (!validationResult.IsValid) return ResultDto<int>.Failure(validationResult.Errors.First().ErrorMessage);


        var leaveRequest = _mapper.Map<LeaveRequest>(request.CreateLeaveRequestDto);
        leaveRequest = await _leaveRequestRepo.Add(leaveRequest);

        var user = await _userRepo.Get(request.UserId);

        var email = new EmailDto
        {
            Destination = user.Email,
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
            throw new Exception(e.Message, e);
        }

        return ResultDto<int>.Success(leaveRequest.Id, "LeaveRequest Created Successfully");
    }
}