using AutoMapper;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.User;
using HR_Management.Application.Features.User.Requests.Queries;
using HR_Management.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HR_Management.Application.Features.User.Handlers.Queries;

public class GetUsersListRequestHandler : IRequestHandler<GetUsersListRequest, PagedResultDto<GetUsersDto>>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepo;

    public GetUsersListRequestHandler(IUserRepository userRepo, IMapper mapper)
    {
        _userRepo = userRepo;
        _mapper = mapper;
    }

    public async Task<PagedResultDto<GetUsersDto>> Handle(GetUsersListRequest request,
        CancellationToken cancellationToken)
    {
        var query = _userRepo.GetUsersWithDetails();
        var totalCount = await query.CountAsync(cancellationToken);
        var pagination = request.Pagination;


        //Dynamic sorting with switch for safety
        var sortTypeLower = (pagination.sortType ?? string.Empty).ToLowerInvariant();
        var isDescending = pagination.isDescending == true;
        query = sortTypeLower switch
        {
            "fullname" => isDescending
                ? query.OrderByDescending(u => u.FullName)
                : query.OrderBy(u => u.FullName),
            "rolename" => isDescending
                ? query.OrderByDescending(u => u.Role.Name)
                : query.OrderBy(u => u.Role.Name),
            "createdat" => isDescending
                ? query.OrderByDescending(u => u.CreatedAt)
                : query.OrderBy(u => u.CreatedAt),
            "updatedat" => isDescending
                ? query.OrderByDescending(u => u.UpdatedAt)
                : query.OrderBy(u => u.UpdatedAt),
            "lastlogin" => isDescending
                ? query.OrderByDescending(u => u.LastLogin)
                : query.OrderBy(u => u.LastLogin),
            "isactive" => isDescending
                ? query.OrderByDescending(u => u.IsActive)
                : query.OrderBy(u => u.IsActive),
            _ => query.OrderByDescending(u => u.Id)
        };

        //search
        if (!string.IsNullOrWhiteSpace(pagination.searchKey))
        {
            var searchKey = pagination.searchKey.Trim().ToLowerInvariant();
            query = query.Where(u =>
                u.FullName.ToLower().Contains(searchKey) ||
                (!string.IsNullOrEmpty(u.Mobile) && u.Mobile.ToLower().Contains(searchKey)) ||
                u.Email.Contains(searchKey) ||
                u.Role.Name.ToLower().Contains(searchKey));
        }

        var pagedQuery = await query.ToPagedAsync(pagination.pageNumber, pagination.pageSize);
        var users = _mapper.Map<List<GetUsersDto>>(pagedQuery.items);

        return new PagedResultDto<GetUsersDto>
        {
            CurrentPage = pagination.pageNumber,
            PageSize = pagination.pageSize,
            Filter =
            [
                new ResponseFilterDto
                {
                    Id = "all",
                    Count = totalCount,
                    Label = "All Users"
                }
            ],
            Items = users,
            TotalPage = pagedQuery.totalPage
        };
    }
}