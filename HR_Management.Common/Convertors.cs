using System.Text;
using PhoneNumbers;

namespace HR_Management.Common;

public static class Convertors
{
    private static readonly PhoneNumberUtil _PhoneUtil = PhoneNumberUtil.GetInstance();

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

    /// <summary>
    ///     convert international phone number to raw format (just national number without country code)
    /// </summary>
    /// <param name="phoneNumber"></param>
    public static string ToRawNationalNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return string.Empty;
        try
        {
            var parsed = _PhoneUtil.Parse(phoneNumber, null);

            if (!_PhoneUtil.IsValidNumber(parsed))
                throw new Exception("Invalid phone number");

            //National number is the number without country code
            return parsed.NationalNumber.ToString();
        }
        catch (NumberParseException)
        {
            throw new ArgumentException("Invalid phone number format.");
        }
    }

    /// <summary>
    ///     return country code of phoneNumber (for example 1 for the USA)
    /// </summary>
    /// <param name="phoneNumber"></param>
    public static int GetCountryCode(string phoneNumber)
    {
        var parsed = _PhoneUtil.Parse(phoneNumber, null);
        return parsed.CountryCode;
    }
}