using AutoMapper;
using HR_Management.Application.Contracts.Persistence;
using HR_Management.Application.DTOs.User.Validator;
using HR_Management.Application.Features.User.Requests.Commands;
using HR_Management.Common;
using MediatR;

namespace HR_Management.Application.Features.User.Handlers.Commands;

public class EditProfileCommandHandler : IRequestHandler<EditProfileCommand, ResultDto>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepo;

    public EditProfileCommandHandler(IMapper mapper, IUserRepository userRepo)
    {
        _mapper = mapper;
        _userRepo = userRepo;
    }

    public async Task<ResultDto> Handle(EditProfileCommand request, CancellationToken cancellationToken)
    {
        var validator = new EditProfileValidator();
        var validationResult = await validator.ValidateAsync(request.EditProfileDto, cancellationToken);
        if (!validationResult.IsValid) return ResultDto.Failure(validationResult.Errors.First().ErrorMessage);

        var user = await _userRepo.GetAsync(request.EditProfileDto.Id);
        if (user == null)
            return ResultDto.Failure("The user was not found.");

        if (!string.IsNullOrWhiteSpace(request.EditProfileDto.Mobile))
        {
            var formatedMobile = Convertors.ToRawNationalNumber(request.EditProfileDto.Mobile);
            var countryCode = Convertors.GetCountryCode(request.EditProfileDto.Mobile);
            request.EditProfileDto.Mobile = formatedMobile;
            request.EditProfileDto.CountryCode = countryCode;
        }

        _mapper.Map(request.EditProfileDto, user);
        await _userRepo.UpdateAsync(user);

        return ResultDto.Success("Profile Updated Correctly.");
    }
}