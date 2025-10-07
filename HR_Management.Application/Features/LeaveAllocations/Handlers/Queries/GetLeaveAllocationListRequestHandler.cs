using AutoMapper;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveAllocation;
using HR_Management.Application.Features.LeaveAllocations.Requests.Queries;
using HR_Management.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HR_Management.Application.Features.LeaveAllocations.Handlers.Queries;

public class GetLeaveAllocationListRequestHandler :
    IRequestHandler<GetLeaveAllocationListRequest, PagedResultDto<LeaveAllocationDto>>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepo;
    private readonly IMapper _mapper;

    public GetLeaveAllocationListRequestHandler(
        ILeaveAllocationRepository leaveAllocation,
        IMapper mapper
    )
    {
        _leaveAllocationRepo = leaveAllocation;
        _mapper = mapper;
    }

    public async Task<PagedResultDto<LeaveAllocationDto>> Handle(GetLeaveAllocationListRequest request,
        CancellationToken cancellationToken)
    {
        var query = _leaveAllocationRepo.GetLeaveAllocationsWithDetails();
        var totalCount = await query.CountAsync(cancellationToken);
        var pagination = request.Pagination;

        // Dynamic sorting with switch for safety 
        var sortTypeLower = (pagination.sortType ?? string.Empty).ToLowerInvariant();
        var isDescending = pagination.isDescending == true;
        query = sortTypeLower switch
        {
            "leavetypename" => isDescending
                ? query.OrderByDescending(la => la.LeaveType.Name)
                : query.OrderBy(l => l.LeaveType.Name),
            "datecreated" => isDescending
                ? query.OrderByDescending(la => la.DateCreated)
                : query.OrderBy(lr => lr.DateCreated),
            "period" => isDescending
                ? query.OrderByDescending(la => la.Period)
                : query.OrderBy(la => la.Period),
            "totaldays" => isDescending
                ? query.OrderByDescending(la => la.TotalDays)
                : query.OrderBy(la => la.TotalDays),
            "fullname" => isDescending
                ? query.OrderByDescending(la => la.User.FullName)
                : query.OrderBy(la => la.User.FullName),
            _ => query.OrderByDescending(la => la.Id)
        };

        //search
        if (!string.IsNullOrWhiteSpace(pagination.searchKey))
        {
            var searchKey = pagination.searchKey.Trim().ToLowerInvariant();
            query = query.Where(la =>
                la.LeaveType.Name.ToLower().Contains(searchKey) ||
                la.User.FullName.ToLower().Contains(searchKey));
        }

        var pagedQuery = await query.ToPagedAsync(pagination.pageNumber, pagination.pageSize);
        var leaveAllocations = _mapper.Map<List<LeaveAllocationDto>>(pagedQuery.items);

        return new PagedResultDto<LeaveAllocationDto>
        {
            CurrentPage = pagination.pageNumber,
            PageSize = pagination.pageSize,
            TotalPage = pagedQuery.totalPage,
            Items = leaveAllocations,
            Filter =
            [
                new ResponseFilterDto
                {
                    Id = "all",
                    Count = totalCount,
                    Label = "All Leave Allocations"
                }
            ]
        };
    }
}