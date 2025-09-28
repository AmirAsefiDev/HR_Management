using AutoMapper;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.User;
using HR_Management.Application.Features.User.Requests.Queries;
using MediatR;

namespace HR_Management.Application.Features.User.Handlers.Queries;

public class GetUserDetailRequestHandler : IRequestHandler<GetUserDetailRequest, GetUserDto>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepo;

    public GetUserDetailRequestHandler(IUserRepository userRepo, IMapper mapper)
    {
        _userRepo = userRepo;
        _mapper = mapper;
    }

    public async Task<GetUserDto> Handle(GetUserDetailRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepo.GetUserWithDetails(request.UserId);
        return _mapper.Map<GetUserDto>(user);
    }
}