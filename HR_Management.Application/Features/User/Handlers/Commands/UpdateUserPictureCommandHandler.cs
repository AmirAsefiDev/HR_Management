using HR_Management.Application.Contracts.Infrastructure.Path;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.User.UpdateUserPicture;
using HR_Management.Application.DTOs.User.Validator;
using HR_Management.Application.Features.User.Requests.Commands;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.User.Handlers.Commands;

public class UpdateUserPictureCommandHandler :
    IRequestHandler<UpdateUserPictureCommand, ResultDto<UpdateUserPictureResponseDto>>
{
    private readonly IPathService _pathService;
    private readonly IUserRepository _userRepo;

    public UpdateUserPictureCommandHandler(IPathService pathService,
        IUserRepository userRepo)
    {
        _pathService = pathService;
        _userRepo = userRepo;
    }

    public async Task<ResultDto<UpdateUserPictureResponseDto>> Handle(UpdateUserPictureCommand request,
        CancellationToken cancellationToken)
    {
        var validator = new UpdateUserPictureValidator();
        var validationResult = await validator.ValidateAsync(request.UpdateUserPictureDto, cancellationToken);
        if (!validationResult.IsValid)
            return ResultDto<UpdateUserPictureResponseDto>.Failure(validationResult.Errors.First().ErrorMessage);

        var user = await _userRepo.Get(request.UpdateUserPictureDto.UserId);
        if (user == null)
            return ResultDto<UpdateUserPictureResponseDto>.Failure("The user was not found.");

        var uploader = new Uploader(_pathService.GetWebRootPath());
        var uploadResult = await uploader.UploadFile(request.UpdateUserPictureDto.Picture, "/images/user/");

        user.Picture = uploadResult.FileNameAddress;
        await _userRepo.Update(user);

        return ResultDto<UpdateUserPictureResponseDto>.Success(
            new UpdateUserPictureResponseDto
            {
                Picture = uploadResult.FileNameAddress
            }, "Your Profile Picture successfully updated.");
    }
}