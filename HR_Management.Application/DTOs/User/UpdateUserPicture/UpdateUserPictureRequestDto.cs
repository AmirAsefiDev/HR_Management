using Microsoft.AspNetCore.Http;

namespace HR_Management.Application.DTOs.User.UpdateUserPicture;

public class UpdateUserPictureRequestDto
{
    public IFormFile Picture { get; set; }
}