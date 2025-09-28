using Microsoft.AspNetCore.Http;

namespace HR_Management.Common;

public class ResultDto
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public int StatusCode { get; set; } = StatusCodes.Status200OK; // 200, 400, 401 

    public static ResultDto Success(string message = "The operation was completed successfully.",
        int statusCode = StatusCodes.Status200OK)
    {
        return new ResultDto
        {
            IsSuccess = true,
            Message = message,
            StatusCode = statusCode
        };
    }

    public static ResultDto Failure(string message, int statusCode = StatusCodes.Status400BadRequest)
    {
        return new ResultDto
        {
            IsSuccess = false,
            Message = message,
            StatusCode = statusCode
        };
    }
}

public class ResultDto<T> : ResultDto
{
    //public bool IsSuccess { get; set; }
    //public string? Message { get; set; }
    //public HttpStatusCode StatusCode { get; set; } // 200, 400, 401
    public T? Data { get; set; }

    public static ResultDto<T> Success(T data, string message = "the operation was completed successfully.",
        int statusCode = 200)
    {
        return new ResultDto<T>
        {
            IsSuccess = true,
            Data = data,
            Message = message,
            StatusCode = statusCode
        };
    }

    public static ResultDto<T> Failure(string message, int statusCode = 400)
    {
        return new ResultDto<T>
        {
            IsSuccess = false,
            Message = message,
            Data = default,
            StatusCode = statusCode
        };
    }
}