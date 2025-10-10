using System.Text;
using HR_Management.Api.Middleware;
using HR_Management.Application;
using HR_Management.Application.Contracts.Infrastructure.Authentication;
using HR_Management.Application.Contracts.Persistence.Context;
using HR_Management.Infrastructure;
using HR_Management.Persistence;
using HR_Management.Persistence.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddTransient<ILeaveManagementDbContext, LeaveManagementDbContext>();
builder.Services.ConfigureApplicationServices();
builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.ConfigureInfrastructureServices(builder.Configuration);

builder.Services.AddSwaggerGen(s =>
{
    s.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "HR_Management.Api.xml"), true);
    s.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "HR_Management.Api",
        Version = "v1"
    });
    var security = new OpenApiSecurityScheme
    {
        Name = "JWT Auth",
        Description = "Please Enter your token.",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    s.AddSecurityDefinition(security.Reference.Id, security);
    s.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { security, new List<string>() }
    });
});

// config Serilog
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341") //SEQ Address
    .CreateLogger();

//replace default ASP.NET Core logger with Serilog
builder.Host.UseSerilog((ctx, lc) =>
    lc.ReadFrom.Configuration(ctx.Configuration));
//config CORS
builder.Services.AddCors(o =>
{
    o.AddPolicy("CorsPolicy",
        b =>
            b.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
});

//config JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(configureOptions =>
{
    configureOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JwtConfig:issuer"],
        ValidAudience = builder.Configuration["JwtConfig:audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:key"])),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        //decrease ClockSkew to 0 because time accuracy
        ClockSkew = TimeSpan.Zero
    };
    configureOptions.SaveToken = true;
    configureOptions.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Log.Error("Authentication failed. Path: {Path}, Error:{Error}",
                context.HttpContext.Request.Path,
                context.Exception.Message);
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            var userId = context.Principal?.FindFirst("userId")?.Value;
            var role = context.Principal?.FindFirst("role")?.Value;

            Log.Information(
                "Token validated. Path: {Path},UserId: {UserId},Role: {Role}, IP: {IP}",
                context.HttpContext.Request.Path,
                userId,
                role,
                context.HttpContext.Connection.RemoteIpAddress?.ToString());

            //validator
            var tokenValidatorService = context.HttpContext.RequestServices.GetRequiredService<ITokenValidator>();
            return tokenValidatorService.ExecuteAsync(context);
        },
        OnChallenge = context =>
        {
            Log.Warning(
                "JWT challenge triggered. Path: {Path}, Scheme: {Scheme}, Error: {Error}, Description : {Description}",
                context.HttpContext.Request.Path,
                context.Scheme.Name,
                context.Error,
                context.ErrorDescription);
            return Task.CompletedTask;
        },
        OnMessageReceived = context =>
        {
            Log.Debug("Message received. Path: {Path}, Token present: {HasToken}",
                context.HttpContext.Request.Path,
                !string.IsNullOrEmpty(context.Token));
            return Task.CompletedTask;
        },
        OnForbidden = context =>
        {
            Log.Warning("Access forbidden. Path: {Path}, User: {User}, IP: {IP}",
                context.HttpContext.Request.Path,
                context.Principal?.Identity?.Name,
                context.HttpContext.Connection.RemoteIpAddress?.ToString());
            return Task.CompletedTask;
        }
    };
});

// builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<CustomAuthorizationMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(s =>
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "HR_Management.Api v1"));

app.UseSerilogRequestLogging(); // Enable Serilog request/response logging

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.MapControllers();

app.MapRazorPages();
app.MapControllerRoute(
    "default",
    "{controller:Home}/{action=Index}/{id?}"
);
app.Run();