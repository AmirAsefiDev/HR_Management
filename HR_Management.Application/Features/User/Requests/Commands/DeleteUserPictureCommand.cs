using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.User.Requests.Commands;

public class DeleteUserPictureCommand : IRequest<ResultDto>
{
    public int UserId { get; set; }
    public string Picture { get; set; }
}