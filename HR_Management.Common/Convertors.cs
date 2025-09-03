using System.Text;

namespace HR_Management.Common;

public static class Convertors
{
    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(plainTextBytes);
    }

    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
        return Encoding.UTF8.GetString(base64EncodedBytes);
    }

    public static string AddZeroToMobile(string mobile)
    {
        if (string.IsNullOrEmpty(mobile))
            return mobile;
        var addedMobile = $"0{mobile}";
        return addedMobile;
    }

    public static string ConvertMobileToRawFormat(string mobile)
    {
        var correctMobileForm = "";
        if (mobile.StartsWith("0")) correctMobileForm = mobile.TrimStart().Substring(1);
        else if (mobile.StartsWith("98")) correctMobileForm = mobile.TrimStart().Substring(2);
        else if (mobile.StartsWith("+98")) correctMobileForm = mobile.TrimStart().Substring(3);
        else correctMobileForm = mobile;
        return correctMobileForm;
    }
}