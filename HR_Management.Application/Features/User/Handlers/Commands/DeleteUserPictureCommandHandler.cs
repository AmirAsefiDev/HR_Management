using HR_Management.Application.Contracts.Infrastructure.Path;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.Features.User.Requests.Commands;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.User.Handlers.Commands;

public class DeleteUserPictureCommandHandler : IRequestHandler<DeleteUserPictureCommand, ResultDto>
{
    private const string DefaultProfilePicturePath = "images/user/default_profile.jpg";
    private readonly IPathService _pathService;
    private readonly IUserRepository _userRepo;

    public DeleteUserPictureCommandHandler(IUserRepository userRepo, IPathService pathService)
    {
        _userRepo = userRepo;
        _pathService = pathService;
    }

    public async Task<ResultDto> Handle(DeleteUserPictureCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Picture))
            return ResultDto.Failure("No picture path was provided.");

        var user = await _userRepo.Get(request.UserId);
        if (user == null)
            return ResultDto.Failure("User not found.");

        var webRootPath = _pathService.GetWebRootPath();
        var imagePath = Path.Combine(webRootPath, request.Picture.TrimStart('/'));
        if (!IsDefaultProfilePicture(request.Picture))
            try
            {
                if (File.Exists(imagePath))
                    File.Delete(imagePath);
            }
            catch (Exception e)
            {
                throw new Exception("Error in deleting picture from server.", e);
            }

        user.Picture = DefaultProfilePicturePath;
        await _userRepo.Update(user);

        return ResultDto.Success("Your picture successfully deleted.");
    }

    private static bool IsDefaultProfilePicture(string picturePath)
    {
        return picturePath.Contains("/images/user/default_profile.jpg", StringComparison.OrdinalIgnoreCase);
    }
}