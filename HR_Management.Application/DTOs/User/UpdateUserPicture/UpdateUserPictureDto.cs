using Microsoft.AspNetCore.Http;

namespace HR_Management.Application.DTOs.User.UpdateUserPicture;

public class UpdateUserPictureDto
{
    public int UserId { get; set; }
    public IFormFile Picture { get; set; }
}