using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.Features.Authentication.Requests.Commands;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.Authentication.Handlers.Commands;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, ResultDto>
{
    private readonly IUserTokenRepository _userTokenRepo;

    public LogoutCommandHandler(IUserTokenRepository userTokenRepo)
    {
        _userTokenRepo = userTokenRepo;
    }

    public async Task<ResultDto> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        await _userTokenRepo.Logout(request.UserId);
        return ResultDto.Success("The user has been successfully logged out.");
    }
}