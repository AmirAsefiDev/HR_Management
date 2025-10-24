using AutoMapper;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveRequestStatusHistory;
using HR_Management.Application.Features.LeaveRequestStatusHistories.Requests.Queries;
using HR_Management.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HR_Management.Application.Features.LeaveRequestStatusHistories.Handlers.Queries;

public class GetLeaveRequestStatusHistoriesListRequestHandler : IRequestHandler<
    GetLeaveRequestStatusHistoriesListRequest, PagedResultDto<LeaveRequestStatusHistoryDto>>
{
    private readonly ILeaveRequestStatusHistoryRepository _leaveRequestStatusHistoryRepo;
    private readonly IMapper _mapper;

    public GetLeaveRequestStatusHistoriesListRequestHandler(
        ILeaveRequestStatusHistoryRepository leaveRequestStatusHistoryRepo,
        IMapper mapper)
    {
        _leaveRequestStatusHistoryRepo = leaveRequestStatusHistoryRepo;
        _mapper = mapper;
    }

    public async Task<PagedResultDto<LeaveRequestStatusHistoryDto>> Handle(
        GetLeaveRequestStatusHistoriesListRequest request, CancellationToken cancellationToken)
    {
        var query = _leaveRequestStatusHistoryRepo.GetLeaveRequestStatusHistoriesWithDetails();
        var totalCount = await query.CountAsync(cancellationToken);
        var pagination = request.Pagination;
        // Dynamic sorting with switch for safety
        var sortTypeLower = (pagination.sortType ?? string.Empty).ToLowerInvariant();
        var isDescending = pagination.isDescending == true;
        query = sortTypeLower switch
        {
            "leaverequestname" => isDescending
                ? query.OrderByDescending(lh => lh.LeaveRequest.RequestComments)
                : query.OrderBy(lh => lh.LeaveRequest.RequestComments),
            "leavestatusname" => isDescending
                ? query.OrderByDescending(lh => lh.LeaveStatus.Name)
                : query.OrderBy(lh => lh.LeaveStatus.Name),
            "comment" => isDescending
                ? query.OrderByDescending(lh => lh.Comment)
                : query.OrderBy(lh => lh.Comment),
            "changername" => isDescending
                ? query.OrderByDescending(lh => lh.User.FullName)
                : query.OrderBy(lh => lh.User.FullName),
            "changedat" => isDescending
                ? query.OrderByDescending(lh => lh.DateCreated)
                : query.OrderBy(lh => lh.DateCreated),
            _ => query.OrderBy(lh => lh.Id)
        };

        //search
        if (!string.IsNullOrWhiteSpace(pagination.searchKey))
        {
            var searchKey = pagination.searchKey.Trim().ToLowerInvariant();
            query = query.Where(lsh =>
                lsh.LeaveStatus.Name.ToLower().Contains(searchKey) ||
                lsh.LeaveRequest.RequestComments.ToLower().Contains(searchKey) ||
                lsh.Comment.ToLower().Contains(searchKey) ||
                lsh.User.FullName.ToLower().Contains(searchKey));
        }

        var pagedQuery = await query.ToPagedAsync(pagination.pageNumber, pagination.pageSize);
        var leaveRequestStatusHistories = _mapper.Map<List<LeaveRequestStatusHistoryDto>>(pagedQuery.items);

        return new PagedResultDto<LeaveRequestStatusHistoryDto>
        {
            TotalPage = pagedQuery.totalPage,
            PageSize = pagination.pageSize,
            CurrentPage = pagination.pageNumber,
            Items = leaveRequestStatusHistories,
            Filter =
            [
                new ResponseFilterDto
                {
                    Id = "all",
                    Count = totalCount,
                    Label = "All Status Changes"
                }
            ]
        };
    }
}