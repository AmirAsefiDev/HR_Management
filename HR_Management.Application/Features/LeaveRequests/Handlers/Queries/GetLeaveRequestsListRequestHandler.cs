using AutoMapper;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveRequest;
using HR_Management.Application.Features.LeaveRequests.Requests.Queries;
using HR_Management.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HR_Management.Application.Features.LeaveRequests.Handlers.Queries;

public class
    GetLeaveRequestsListRequestHandler : IRequestHandler<GetLeaveRequestsListRequest,
    PagedResultDto<LeaveRequestListDto>>
{
    private readonly ILeaveRequestRepository _leaveRequestRepo;
    private readonly IMapper _mapper;

    public GetLeaveRequestsListRequestHandler(ILeaveRequestRepository leaveRequestRepo, IMapper mapper)
    {
        _leaveRequestRepo = leaveRequestRepo;
        _mapper = mapper;
    }

    public async Task<PagedResultDto<LeaveRequestListDto>> Handle(GetLeaveRequestsListRequest request,
        CancellationToken cancellationToken)
    {
        var query = _leaveRequestRepo.GetLeaveRequestsWithDetails();
        var totalCount = await query.CountAsync(cancellationToken);
        var pagination = request.Pagination;

        // Dynamic sorting with switch for safety 
        var sortTypeLower = (pagination.sortType ?? string.Empty).ToLowerInvariant();
        var isDescending = pagination.isDescending == true;
        query = sortTypeLower switch
        {
            "leavetypename" => isDescending
                ? query.OrderByDescending(lr => lr.LeaveType.Name)
                : query.OrderBy(lr => lr.LeaveType.Name),
            "daterequested" => isDescending
                ? query.OrderByDescending(lr => lr.DateRequested)
                : query.OrderBy(lr => lr.DateRequested),
            "leavestatusname" => isDescending
                ? query.OrderByDescending(lr => lr.LeaveStatus.Name)
                : query.OrderBy(lr => lr.LeaveStatus.Name),
            "creatorname" => isDescending
                ? query.OrderByDescending(lr => lr.User.FullName)
                : query.OrderBy(lr => lr.User.FullName),
            _ => query.OrderByDescending(lr => lr.Id)
        };

        //search
        if (!string.IsNullOrWhiteSpace(pagination.searchKey))
        {
            var searchKey = pagination.searchKey.Trim().ToLowerInvariant();
            query = query.Where(lr =>
                lr.User.FullName.ToLower().Contains(searchKey) ||
                lr.LeaveType.Name.ToLower().Contains(searchKey) ||
                lr.LeaveStatus.Name.ToLower().Contains(searchKey));
        }

        var pagedQuery = await query.ToPagedAsync(pagination.pageNumber, pagination.pageSize);
        var leaveRequests = _mapper.Map<List<LeaveRequestListDto>>(pagedQuery.items);

        return new PagedResultDto<LeaveRequestListDto>
        {
            CurrentPage = pagination.pageNumber,
            PageSize = pagination.pageSize,
            Filter =
            [
                new ResponseFilterDto
                {
                    Id = "all",
                    Count = totalCount,
                    Label = "AllOfLeaveRequests"
                }
            ],
            Items = leaveRequests,
            TotalPage = pagedQuery.totalPage
        };
    }
}