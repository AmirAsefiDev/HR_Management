using System.Security.Claims;
using System.Text.Encodings.Web;
using HR_Management.Application.Contracts.Infrastructure.Authentication;
using HR_Management.Persistence.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HR_Management.UnitTests.API.IntegrationTests.Common;

public class CustomWebApplicationFactory
    : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor =
                services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<LeaveManagementDbContext>));

            if (descriptor != null)
                services.Remove(descriptor);

            var provider = services.SingleOrDefault(d =>
                d.ServiceType == typeof(LeaveManagementDbContext));
            if (provider != null)
                services.Remove(provider);

            services.AddDbContext<LeaveManagementDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Test";
                options.DefaultChallengeScheme = "Test";
            }).AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", _ => { });

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<LeaveManagementDbContext>();

            db.Database.EnsureCreated();
        });
        builder.UseEnvironment("Test");
    }
}

public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IRolePermissionService _rolePermissionService;

    public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
        UrlEncoder encoder, ISystemClock clock, IRolePermissionService rolePermissionService) : base(options, logger,
        encoder, clock)
    {
        _rolePermissionService = rolePermissionService;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var claims = new List<Claim>
        {
            new("sub", "1"),
            new("name", "Amir Asefi"),
            new("role", "Admin")
        };

        //add permission dynamically by role
        var permissions = _rolePermissionService.GetPermissionsByRole("Admin");
        foreach (var permission in permissions) claims.Add(new Claim("permission", permission));


        var identity = new ClaimsIdentity(claims, "Test");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "Test");

        var result = AuthenticateResult.Success(ticket);
        return Task.FromResult(result);
    }
}