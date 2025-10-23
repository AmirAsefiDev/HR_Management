using AutoMapper;
using HR_Management.Application.Profiles;

namespace HR_Management.UnitTests.Application.UnitTests;

public abstract class TestBase
{
    protected readonly IMapper _mapper;

    protected TestBase()
    {
        var mapperConfig = new MapperConfiguration(config => { config.AddProfile<MappingProfile>(); });
        _mapper = mapperConfig.CreateMapper();
    }
}