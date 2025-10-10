using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace HR_Management.Application;

public static class ApplicationServicesRegistration
{
    public static void ConfigureApplicationServices(this IServiceCollection services)
    {
        //It's added here individually.
        //services.AddAutoMapper(typeof(MappingProfile));

        //here it's come to find in current assembly to each Profile of AutoMapper.
        //after found them config them in Program.cs.
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddHttpContextAccessor();

        //Config MediateR to connect handlers which create by us from APIs & controllers.
        services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}