using FluentValidation.Results;

namespace HR_Management.Application.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors) Errors.Add(error.ErrorMessage);
    }

    public List<string> Errors { get; set; } = new();
}