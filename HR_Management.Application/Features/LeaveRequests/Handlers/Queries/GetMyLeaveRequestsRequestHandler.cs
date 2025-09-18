using AutoMapper;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveRequest;
using HR_Management.Application.Features.LeaveRequests.Requests.Queries;
using HR_Management.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HR_Management.Application.Features.LeaveRequests.Handlers.Queries;

public class
    GetMyLeaveRequestsRequestHandler : IRequestHandler<GetMyLeaveRequestsRequest, PagedResultDto<LeaveRequestDto>>
{
    private readonly ILeaveRequestRepository _leaveRequestRepo;
    private readonly IMapper _mapper;

    public GetMyLeaveRequestsRequestHandler(ILeaveRequestRepository leaveRequestRepo, IMapper mapper)
    {
        _leaveRequestRepo = leaveRequestRepo;
        _mapper = mapper;
    }

    public async Task<PagedResultDto<LeaveRequestDto>> Handle(GetMyLeaveRequestsRequest request,
        CancellationToken cancellationToken)
    {
        var query = _leaveRequestRepo.GetMyLeaveRequests(request.UserId);
        var totalCount = await query.CountAsync(cancellationToken);

        var pagination = request.Pagination;
        // Dynamic sorting with switch for safety 
        var sortTypeLower = (pagination.sortType ?? string.Empty).ToLowerInvariant();
        var isDescending = pagination.isDescending == true;
        query = sortTypeLower switch
        {
            "daterequested" => isDescending
                ? query.OrderByDescending(lr => lr.DateRequested)
                : query.OrderBy(lr => lr.DateRequested),
            "requestcomments" => isDescending
                ? query.OrderByDescending(lr => lr.RequestComments)
                : query.OrderBy(lr => lr.RequestComments),
            "dateactioned" => isDescending
                ? query.OrderByDescending(lr => lr.DateActioned)
                : query.OrderBy(lr => lr.DateActioned),
            "leavetypename" => isDescending
                ? query.OrderByDescending(lr => lr.LeaveType.Name)
                : query.OrderBy(lr => lr.LeaveType.Name),
            "leavestatusname" => isDescending
                ? query.OrderByDescending(lr => lr.LeaveStatus.Name)
                : query.OrderBy(lr => lr.LeaveStatus.Name),
            "startdate" => isDescending
                ? query.OrderByDescending(lr => lr.StartDate)
                : query.OrderBy(lr => lr.StartDate),
            "enddate" => isDescending
                ? query.OrderByDescending(lr => lr.EndDate)
                : query.OrderBy(lr => lr.EndDate),
            _ => query.OrderByDescending(lr => lr.Id)
        };

        //search
        if (!string.IsNullOrWhiteSpace(pagination.searchKey))
        {
            var searchKey = pagination.searchKey.Trim().ToLowerInvariant();
            query = query.Where(lr =>
                lr.LeaveStatus.Name.ToLower().Contains(searchKey) ||
                lr.LeaveType.Name.ToLower().Contains(searchKey) ||
                lr.RequestComments.ToLower().Contains(searchKey));
        }

        var pagedQuery = await query.ToPagedAsync(pagination.pageNumber, pagination.pageSize);
        var leaveRequests = _mapper.Map<List<LeaveRequestDto>>(pagedQuery.items);
        return new PagedResultDto<LeaveRequestDto>
        {
            CurrentPage = pagination.pageNumber,
            PageSize = pagination.pageSize,
            Filter =
            [
                new ResponseFilterDto
                {
                    Id = "all",
                    Count = totalCount,
                    Label = "AllOfMyLeaveRequest"
                }
            ],
            Items = leaveRequests,
            TotalPage = pagedQuery.totalPage
        };
    }
}