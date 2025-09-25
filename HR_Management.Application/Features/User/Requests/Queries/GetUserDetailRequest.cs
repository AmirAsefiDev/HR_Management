using HR_Management.Application.DTOs.User;
using MediatR;

namespace HR_Management.Application.Features.User.Requests.Queries;

public class GetUserDetailRequest : IRequest<GetUserDto>
{
    public int UserId { get; set; }
}