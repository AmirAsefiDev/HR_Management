using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace HR_Management.Application;

public static class ApplicationServicesRegistration
{
    public static void ConfigureApplicationServicesRegistration(this IServiceCollection services)
    {
        // اینجا به صورت تکی اضافه میشود.
        //services.AddAutoMapper(typeof(MappingProfile));
        // اینجا می آید در اسمبلی فعلی می گردد و هر تعداد پروفایل AutoMapper را پیدا می کند و آن ها را کانفیگ می کند.
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}