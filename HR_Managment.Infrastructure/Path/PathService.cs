using HR_Management.Application.Contracts.Infrastructure.Path;
using Microsoft.AspNetCore.Hosting;

namespace HR_Management.Infrastructure.Path;

public class PathService : IPathService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public PathService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public string GetWebRootPath()
    {
        return _webHostEnvironment.WebRootPath;
    }

    public string GetContentPath()
    {
        return _webHostEnvironment.ContentRootPath;
    }
}