using HR_Management.Application.DTOs.Authentication.RefreshToken;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.Authentication.Requests.Commands;

public class RefreshTokenCommand : IRequest<ResultDto<RefreshTokenDto>>
{
    public RefreshTokenRequestDto RefreshTokenRequestDto { get; set; }
}