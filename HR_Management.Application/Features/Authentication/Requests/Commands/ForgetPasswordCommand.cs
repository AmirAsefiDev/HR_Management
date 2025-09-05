using HR_Management.Application.DTOs.Authentication.ForgetPassword;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.Authentication.Requests.Commands;

public class ForgetPasswordCommand : IRequest<ResultDto>
{
    public ForgetPasswordRequestDto ForgetPasswordRequestDto { get; set; }
}