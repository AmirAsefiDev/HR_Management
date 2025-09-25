using PhoneNumbers;

namespace HR_Management.Application.DTOs.User.Validator;

public static class PhoneNumberValidator
{
    public static readonly PhoneNumberUtil _PhoneUtil = PhoneNumberUtil.GetInstance();

    public static bool IsValidInternationalNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return false;

        try
        {
            // Parse phone number to international format
            var parsedPhoneNumber = _PhoneUtil.Parse(phoneNumber, null);
            // validate that phoneNumber is valid or not
            return _PhoneUtil.IsValidNumber(parsedPhoneNumber);
        }
        catch
        {
            return false;
        }
    }
}