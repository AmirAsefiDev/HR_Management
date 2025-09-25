using HR_Management.Application.DTOs.User.EditProfile;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.User.Requests.Commands;

public class EditProfileCommand : IRequest<ResultDto>
{
    public EditProfileDto EditProfileDto { get; set; }
}