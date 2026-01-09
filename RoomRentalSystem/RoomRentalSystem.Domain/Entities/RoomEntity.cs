using RoomRentalSystem.Domain.Constants;
using RoomRentalSystem.Domain.Exceptions;

namespace RoomRentalSystem.Domain.Entities;

// TODO: Solid => Single Responsibility Principle, add RoomDetails class
public class RoomEntity : BaseEntity
{
    private const int MinPricePerDay = 1;
    private const int MinArea = 1;
    private const int MinRoomCount = 1;

    public string Title { get; private set; }
    public decimal PricePerDay { get; private set; }
    public string Description { get; private set; }
    public int Floor { get; private set; }
    public double Area { get; private set; }
    public int RoomCount { get; private set; }
    public string Amenities { get; private set; }
    public string Address { get; private set; }
    public RoomRentalStatus Status { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsFavorite { get; private set; }
    public Guid UserId { get; private set; }
    public UserEntity User { get; private set; }
    public Guid ImageId { get; private set; }
    public ImageEntity Image { get; private set; }
    public ICollection<ImageEntity> Images { get; private set; } = [];
    public ICollection<BookingEntity> Bookings { get; private set; } = [];
    public ICollection<ReviewEntity> Reviews { get; private set; } = [];

    private RoomEntity() { }
        
    public static RoomEntity Create(
        Guid userId,
        Guid imageId,
        string title,
        decimal pricePerDay,
        string description,
        int floor,
        double area,
        int roomCount,
        string address,
        string amenities = "")
    {
        if (userId == Guid.Empty)
        {
            throw new DomainException(nameof(userId));
        }
        if (imageId == Guid.Empty)
        {
            throw new DomainException(nameof(imageId));
        }
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new DomainException(nameof(title));
        }
        if (pricePerDay < MinPricePerDay)
        {
            throw new DomainException(nameof(pricePerDay));
        }
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new DomainException(nameof(description));
        }
        if (area < MinArea)
        {
            throw new DomainException(nameof(area));
        }
        if (roomCount < MinRoomCount)
        {
            throw new DomainException(nameof(roomCount));
        }
        if (string.IsNullOrWhiteSpace(address))
        {
            throw new DomainException(nameof(address));
        }

        return new RoomEntity
        {
            UserId = userId,
            ImageId = imageId,
            Title = title,
            PricePerDay = pricePerDay,
            Description = description,
            Floor = floor,
            Area = area,
            RoomCount = roomCount,
            Amenities = amenities,
            Address = address,
            Status = RoomRentalStatus.Pending,
            IsActive = true,
            IsFavorite = false
        };
    }
}