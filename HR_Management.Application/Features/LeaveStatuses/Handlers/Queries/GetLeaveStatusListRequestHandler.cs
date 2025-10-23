using AutoMapper;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveStatus;
using HR_Management.Application.Features.LeaveStatuses.Requests.Queries;
using HR_Management.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HR_Management.Application.Features.LeaveStatuses.Handlers.Queries;

public class
    GetLeaveStatusListRequestHandler : IRequestHandler<GetLeaveStatusListRequest, PagedResultDto<LeaveStatusDto>>
{
    private readonly ILeaveStatusRepository _leaveStatusRepo;
    private readonly IMapper _mapper;

    public GetLeaveStatusListRequestHandler(ILeaveStatusRepository leaveStatusRepo, IMapper mapper)
    {
        _leaveStatusRepo = leaveStatusRepo;
        _mapper = mapper;
    }

    public async Task<PagedResultDto<LeaveStatusDto>> Handle(GetLeaveStatusListRequest request,
        CancellationToken cancellationToken)
    {
        var query = _leaveStatusRepo.GetLeaveStatusesWithDetails();

        var totalCount = await query.CountAsync(cancellationToken);
        var pagination = request.Pagination;

        // Dynamic sorting with switch for safety 
        var sortTypeLower = (pagination.sortType ?? string.Empty).ToLowerInvariant();
        var isDescending = pagination.isDescending == true;
        query = sortTypeLower switch
        {
            "name" => isDescending
                ? query.OrderByDescending(ls => ls.Name)
                : query.OrderBy(ls => ls.Name),
            "description" => isDescending
                ? query.OrderByDescending(ls => ls.Description)
                : query.OrderBy(ls => ls.Description),
            "datecreated" => isDescending
                ? query.OrderByDescending(ls => ls.DateCreated)
                : query.OrderBy(ls => ls.DateCreated),
            _ => query.OrderByDescending(ls => ls.Id)
        };


        //search
        if (!string.IsNullOrWhiteSpace(pagination.searchKey))
        {
            var searchKey = pagination.searchKey.Trim().ToLowerInvariant();
            query = query.Where(ls =>
                ls.Name.ToLower().Contains(searchKey) ||
                (!string.IsNullOrWhiteSpace(ls.Description) && ls.Description.ToLower().Contains(searchKey)));
        }

        var pagedQuery = await query.ToPagedAsync(pagination.pageNumber, pagination.pageSize);
        var leaveStatuses = _mapper.Map<List<LeaveStatusDto>>(pagedQuery.items);

        return new PagedResultDto<LeaveStatusDto>
        {
            CurrentPage = pagination.pageNumber,
            PageSize = pagination.pageSize,
            TotalPage = pagedQuery.totalPage,
            Items = leaveStatuses,
            Filter =
            [
                new ResponseFilterDto
                {
                    Id = "all",
                    Count = totalCount,
                    Label = "All Leave Statuses"
                }
            ]
        };
    }
}