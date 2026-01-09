using RoomRentalSystem.Domain.Exceptions;
using RoomRentalSystem.Domain.ValidationRules;
using static System.Text.RegularExpressions.Regex;

namespace RoomRentalSystem.Domain.Entities;

public class UserEntity : BaseEntity
{
    public string PhoneNumber { get; init; }
    public string Email { get; init; }
    public string PasswordHash { get; init; }
    public Guid? ImageId { get; init; }
    public ImageEntity? Image { get; init; }
    public ICollection<RoleEntity> Roles { get; init; } = [];
    public ICollection<RoomEntity> Rooms { get; init; } = [];
    public ICollection<ReviewEntity> Reviews { get; init; } = [];
    public ICollection<BookingEntity> Bookings { get; init; } = [];

    private UserEntity() { }

    public static UserEntity? Create(
        string phoneNumber,
        string email,
        string passwordHash,
        ICollection<RoleEntity> roles)
    {
        if (!IsValidPhoneNumber(phoneNumber))
        {
            throw new DomainException(nameof(phoneNumber));
        }
        if (!IsValidEmail(email))
        {
            throw new DomainException(nameof(email));
        }
        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            throw new DomainException(nameof(passwordHash));
        }

        var rolesArray = roles ?? [];
        if (rolesArray.Count != 0)
        {
            throw new DomainException(nameof(roles));
        }

        return new UserEntity
        {
            PasswordHash = passwordHash,
            PhoneNumber = phoneNumber,
            Email = email,
            Roles = rolesArray
        };
    }
    // TODO: Create new email and phone  records
    private static bool IsValidEmail(string email) =>
        !string.IsNullOrWhiteSpace(email) && IsMatch(email, RegularExpressionsForValidation.EmailValidationPattern);
    private static bool IsValidPhoneNumber(string phoneNumber) =>
        !string.IsNullOrWhiteSpace(phoneNumber) && IsMatch(phoneNumber, RegularExpressionsForValidation.BelarusPhoneNumberValidationPattern);
}