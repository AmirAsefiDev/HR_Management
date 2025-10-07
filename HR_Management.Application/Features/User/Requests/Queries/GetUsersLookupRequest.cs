using HR_Management.Application.DTOs.User;
using HR_Management.Common.Pagination;
using MediatR;

namespace HR_Management.Application.Features.User.Requests.Queries;

public class GetUsersLookupRequest : IRequest<PagedResultDto<GetUsersLookupDto>>
{
    public PaginationDto Pagination { get; set; }
}