using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.Authentication.Requests.Commands;

public class LogoutCommand : IRequest<ResultDto>
{
    public int UserId { get; set; }
}