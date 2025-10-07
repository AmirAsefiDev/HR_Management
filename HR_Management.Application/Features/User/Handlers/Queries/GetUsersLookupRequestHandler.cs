using AutoMapper;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.User;
using HR_Management.Application.Features.User.Requests.Queries;
using HR_Management.Common.Pagination;
using MediatR;

namespace HR_Management.Application.Features.User.Handlers.Queries;

public class GetUsersLookupRequestHandler : IRequestHandler<GetUsersLookupRequest, PagedResultDto<GetUsersLookupDto>>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepo;

    public GetUsersLookupRequestHandler(IUserRepository userRepo, IMapper mapper)
    {
        _userRepo = userRepo;
        _mapper = mapper;
    }

    public async Task<PagedResultDto<GetUsersLookupDto>> Handle(GetUsersLookupRequest request,
        CancellationToken cancellationToken)
    {
        var query = _userRepo.GetUsersWithDetails();

        var pagination = request.Pagination;

        //Dynamic sorting with switch for safety
        var sortTypeLower = (pagination.sortType ?? string.Empty).ToLowerInvariant();
        var isDescending = pagination.isDescending == true;
        query = sortTypeLower switch
        {
            "fullname" => isDescending
                ? query.OrderByDescending(u => u.FullName)
                : query.OrderBy(u => u.FullName),
            _ => query.OrderByDescending(u => u.Id)
        };

        //search
        if (!string.IsNullOrWhiteSpace(pagination.searchKey))
        {
            var searchKey = pagination.searchKey.Trim().ToLowerInvariant();
            query = query.Where(u =>
                u.FullName.ToLower().Contains(searchKey) ||
                u.Email.Contains(searchKey));
        }


        var pagedQuery = await query.ToPagedAsync(pagination.pageNumber, pagination.pageSize);
        var users = _mapper.Map<List<GetUsersLookupDto>>(pagedQuery.items);

        return new PagedResultDto<GetUsersLookupDto>
        {
            CurrentPage = pagination.pageNumber,
            PageSize = pagination.pageSize,
            Filter = [],
            Items = users,
            TotalPage = pagedQuery.totalPage
        };
    }
}