using FluentValidation;
using HR_Management.Application.DTOs.User.UpdateUserPicture;

namespace HR_Management.Application.DTOs.User.Validator;

public class UpdateUserPictureValidator : AbstractValidator<UpdateUserPictureDto>
{
    public UpdateUserPictureValidator()
    {
        RuleFor(u => u.Picture)
            .NotNull()
            .WithMessage("Please upload your photo.");

        //limitation size: 1MB
        long maxSize = 1 * 1024 * 1024;
        RuleFor(u => u.Picture)
            .Must(p => p == null || p.Length <= maxSize)
            .WithMessage(
                "Photo must not exceed 1MB. Please reduce the size or choose another one photo.");

        //checks fileName which are allowed or forbidden
        var forbiddenExtensions = new[] { ".exe", ".bat", ".batch" };
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

        RuleFor(u => u.Picture)
            .Must(p =>
            {
                if (p == null) return true;

                var extension = Path.GetExtension(p.FileName.ToLowerInvariant());
                //validate that it's not forbidden
                if (forbiddenExtensions.Contains(extension))
                    return false;

                //it must be valid format of photo
                if (!allowedExtensions.Contains(extension))
                    return false;

                return true;
            })
            .WithMessage("Invalid photo format. Allowed formats: jpg, jpeg, png, gif.");
    }
}