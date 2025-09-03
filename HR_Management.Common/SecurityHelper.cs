using System.Security.Cryptography;
using System.Text;

namespace HR_Management.Common;

public static class SecurityHelper
{
    private static readonly RandomNumberGenerator random = RandomNumberGenerator.Create();

    public static string GetSHA256Hash(string value)
    {
        var algorithm = new SHA256CryptoServiceProvider();
        var byteValue = Encoding.UTF8.GetBytes(value);
        var byteHash = algorithm.ComputeHash(byteValue);
        return Convert.ToBase64String(byteHash);
    }
}