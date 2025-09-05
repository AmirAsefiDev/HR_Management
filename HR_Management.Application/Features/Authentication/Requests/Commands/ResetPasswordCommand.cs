using HR_Management.Application.DTOs.Authentication.ResetPassword;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.Authentication.Requests.Commands;

public class ResetPasswordCommand : IRequest<ResultDto>
{
    public ResetPasswordRequestDto ResetPasswordRequestDto { get; set; }
}