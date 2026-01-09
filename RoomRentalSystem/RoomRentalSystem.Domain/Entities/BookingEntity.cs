using RoomRentalSystem.Domain.Exceptions;

namespace RoomRentalSystem.Domain.Entities;

public class BookingEntity : BaseEntity
{
    private const decimal MinTotalPrice = 1;

    public DateRangeEntity BookingDate { get; set; }
    public decimal TotalPrice { get; private set; } // Price in major units
    public Guid UserId { get; private set; }
    public UserEntity User { get; private set; }
    public Guid RoomId { get; private set; }
    public RoomEntity Room { get; private set; }

    private BookingEntity() { }

    public static BookingEntity Create(
        Guid userId,
        Guid roomId,
        DateTime startDate, 
        DateTime endDate,
        decimal totalPrice
    )
    {
        if (userId == Guid.Empty)
        {
            throw new DomainException(nameof(userId));
        }
        if (roomId == Guid.Empty)
        {
            throw new DomainException(nameof(roomId));
        }
        if (totalPrice < MinTotalPrice)
        {
            throw new InvalidTotalPriceException(totalPrice, MinTotalPrice);
        }

        var bookingDate = DateRangeEntity.Create(startDate, endDate);

        return new BookingEntity
        {
            UserId = userId,
            RoomId = roomId,
            BookingDate = bookingDate,
            TotalPrice = totalPrice,
        };
    }
}