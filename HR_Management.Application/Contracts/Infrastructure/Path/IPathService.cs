namespace HR_Management.Application.Contracts.Infrastructure.Path;

public interface IPathService
{
    string GetWebRootPath();
    string GetContentPath();
}