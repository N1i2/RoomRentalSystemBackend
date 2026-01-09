namespace RoomRentalSystem.Domain.ValidationRules;

public static class RegularExpressionsForValidation
{
    public const string EmailValidationPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
    public const string BelarusPhoneNumberValidationPattern = @"^\+375\s?(17|25|29|33|44)\d{3}-?\d{2}-?\d{2}$";
}