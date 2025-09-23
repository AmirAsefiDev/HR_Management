namespace HR_Management.Application.Contracts.Infrastructure.Authentication;

public interface IRolePermissionService
{
    IEnumerable<string> GetPermissionsByRole(string role);
}