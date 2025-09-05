using HR_Management.Application.DTOs.Authentication.Signup;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.Authentication.Requests.Commands;

public class SignupCommand : IRequest<ResultDto<SignupDto>>
{
    public SignupRequestDto SignupRequestDto { get; set; }
}