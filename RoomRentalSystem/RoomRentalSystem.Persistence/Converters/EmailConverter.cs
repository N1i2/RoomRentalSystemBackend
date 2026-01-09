using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RoomRentalSystem.Domain.ValidationRules;
using RoomRentalSystem.Persistence.Exceptions;
using static System.Text.RegularExpressions.Regex;

namespace RoomRentalSystem.Persistence.Converters;

public class EmailConverter() : ValueConverter<string, string>(v => ValidateEmail(v), v => v)
{
    private static string ValidateEmail(string email)
    {
        if (!IsMatch(email, RegularExpressionsForValidation.EmailValidationPattern))
        {
            throw new PersistenceException("Incorrect email");
        }

        return email;
    }
}