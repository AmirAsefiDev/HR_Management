using HR_Management.Application.DTOs.User.UpdateUserPicture;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.User.Requests.Commands;

public class UpdateUserPictureCommand : IRequest<ResultDto<UpdateUserPictureResponseDto>>
{
    public UpdateUserPictureDto UpdateUserPictureDto { get; set; }
}