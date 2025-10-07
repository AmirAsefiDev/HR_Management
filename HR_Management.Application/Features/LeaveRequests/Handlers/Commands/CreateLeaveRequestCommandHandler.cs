using AutoMapper;
using ERP.Application.Interfaces.Email;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveRequest.CreateLeaveRequest;
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
    private readonly ILeaveAllocationRepository _leaveAllocationRepo;
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
        IUserRepository userRepo,
        ILeaveAllocationRepository leaveAllocationRepo)
    {
        _leaveRequestRepo = leaveRequestRepo;
        _mapper = mapper;
        _leaveTypeRepo = leaveTypeRepo;
        _leaveStatusRepo = leaveStatusRepo;
        _emailService = emailService;
        _logger = logger;
        _userRepo = userRepo;
        _leaveAllocationRepo = leaveAllocationRepo;
    }

    public async Task<ResultDto<int>> Handle(CreateLeaveRequestCommand request,
        CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveRequestDtoValidator(_leaveTypeRepo, _leaveStatusRepo);
        var validationResult = await validator.ValidateAsync(request.CreateLeaveRequestDto, cancellationToken);

        if (!validationResult.IsValid) return ResultDto<int>.Failure(validationResult.Errors.First().ErrorMessage);


        double requestedAmount;
        switch (request.CreateLeaveRequestDto.LeaveMeasureType)
        {
            //calculate and receive total days with considering last day of leave request.
            case LeaveMeasureType.DayBased:
                requestedAmount = (request.CreateLeaveRequestDto.EndDate.Date -
                                   request.CreateLeaveRequestDto.StartDate.Date).TotalDays + 1;
                break;
            case LeaveMeasureType.HourBased:

                var leaveType = await _leaveTypeRepo.GetAsync(request.CreateLeaveRequestDto.LeaveTypeId);
                var totalHours = (request.CreateLeaveRequestDto.EndDate - request.CreateLeaveRequestDto.StartDate)
                    .TotalHours;

                if (leaveType.HoursPerDay <= 0)
                    throw new Exception("Leave type must define valid HoursPerDay for hourly calculation.");

                requestedAmount = totalHours / leaveType.HoursPerDay;

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        var hasSufficientAllocation = await _leaveAllocationRepo.HasSufficientAllocationAsync(
            request.CreateLeaveRequestDto.UserId,
            request.CreateLeaveRequestDto.LeaveTypeId,
            (int)requestedAmount);

        if (!hasSufficientAllocation)
            return ResultDto<int>.Failure("You don't have enough leave allocation for this request.");

        var leaveRequest = _mapper.Map<LeaveRequest>(request.CreateLeaveRequestDto);
        leaveRequest = await _leaveRequestRepo.AddAsync(leaveRequest);

        var user = await _userRepo.GetAsync(leaveRequest.UserId);

        var email = new EmailDto
        {
            Destination = user.Email,
            Title = "Leave Request Submitted",
            MessageBody =
                $@"
                <p>Your leave request for <b>{request.CreateLeaveRequestDto.StartDate:yyyy-MM-dd}</b> 
                to <b>{request.CreateLeaveRequestDto.EndDate:yyyy-MM-dd}</b> has been submitted.</p>

                <p>Please wait until the HR department reviews your request.  
                After that, we will notify you of the result.</p>

                <p>Thank you 🙏</p>"
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

        return ResultDto<int>.Success(leaveRequest.Id, "LeaveRequest Created Successfully", 201);
    }
}