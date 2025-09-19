using AutoMapper;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.LeaveType;
using HR_Management.Application.Features.LeaveTypes.Requests.Queries;
using HR_Management.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HR_Management.Application.Features.LeaveTypes.Handlers.Queries;

public class GetLeaveTypeListRequestHandler :
    IRequestHandler<GetLeaveTypeListRequest, PagedResultDto<LeaveTypeDto>>
{
    private readonly ILeaveTypeRepository _leaveTypeRepo;
    private readonly IMapper _mapper;

    public GetLeaveTypeListRequestHandler(
        ILeaveTypeRepository leaveTypeRepo,
        IMapper mapper
    )
    {
        _leaveTypeRepo = leaveTypeRepo;
        _mapper = mapper;
    }

    public async Task<PagedResultDto<LeaveTypeDto>> Handle(GetLeaveTypeListRequest request,
        CancellationToken cancellationToken)
    {
        var query = _leaveTypeRepo.GetLeaveTypesWithDetails();
        var totalCount = await query.CountAsync(cancellationToken);
        var pagination = request.Pagination;

        // Dynamic sorting with switch for safety 
        var sortTypeLower = (pagination.sortType ?? string.Empty).ToLowerInvariant();
        var isDescending = pagination.isDescending == true;
        query = sortTypeLower switch
        {
            "name" => isDescending
                ? query.OrderByDescending(lt => lt.Name)
                : query.OrderBy(lt => lt.Name),
            "datecreated" => isDescending
                ? query.OrderByDescending(lt => lt.DateCreated)
                : query.OrderBy(lt => lt.DateCreated),
            "defaultday" => isDescending
                ? query.OrderByDescending(lt => lt.DefaultDay)
                : query.OrderBy(lt => lt.DefaultDay),
            _ => query.OrderByDescending(lt => lt.Id)
        };

        //search
        if (!string.IsNullOrWhiteSpace(pagination.searchKey))
        {
            var searchKey = pagination.searchKey.Trim().ToLowerInvariant();
            query = query.Where(lt => lt.Name.ToLower().Contains(searchKey));
        }

        var pagedQuery = await query.ToPagedAsync(pagination.pageNumber, pagination.pageSize);
        var leaveTypes = _mapper.Map<List<LeaveTypeDto>>(pagedQuery.items);

        return new PagedResultDto<LeaveTypeDto>
        {
            CurrentPage = pagination.pageNumber,
            PageSize = pagination.pageSize,
            TotalPage = pagedQuery.totalPage,
            Items = leaveTypes,
            Filter =
            [
                new ResponseFilterDto
                {
                    Id = "all",
                    Count = totalCount,
                    Label = "AllOfLeaveType"
                }
            ]
        };
    }
}