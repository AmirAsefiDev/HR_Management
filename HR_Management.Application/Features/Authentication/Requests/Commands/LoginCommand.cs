using HR_Management.Application.DTOs.Authentication.Login;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.Authentication.Requests.Commands;

public class LoginCommand : IRequest<ResultDto<LoginDto>>
{
    public LoginRequestDto LoginRequestDto { get; set; }
}