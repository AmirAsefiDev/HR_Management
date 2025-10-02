using System.Reflection;
using HR_Management.Common.Security;
using Microsoft.AspNetCore.Authorization;

namespace HR_Management.Infrastructure.Authentication;

public static class AuthorizationPoliciesExtension
{
    public static void AddPermissionPolicies(this AuthorizationOptions options)
    {
        var permissions = typeof(Permissions)
            .GetFields(
                BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.FlattenHierarchy)
            .Where(fi => fi.IsLiteral && !fi.IsInitOnly)
            .Select(fi => fi.GetValue(null)?.ToString())
            .Where(p => !string.IsNullOrWhiteSpace(p))
            .ToList();

        foreach (var permission in permissions)
            options.AddPolicy(permission, policy => policy.RequireClaim("permission", permission));
    }
}