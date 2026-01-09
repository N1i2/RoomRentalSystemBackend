using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RoomRentalSystem.Domain.ValidationRules;
using RoomRentalSystem.Persistence.Exceptions;
using static System.Text.RegularExpressions.Regex;

namespace RoomRentalSystem.Persistence.Converters;

public class PhoneNumberConverter():ValueConverter<string, string>(pn => ValidatePhoneNumber(pn), pn => pn)
{
    public static string ValidatePhoneNumber(string phoneNumber)
    {
        if (!IsMatch(phoneNumber, RegularExpressionsForValidation.BelarusPhoneNumberValidationPattern))
        {
            throw new PersistenceException("Incorrect phone number");
        }

        return phoneNumber;
    }
}