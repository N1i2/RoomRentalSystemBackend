using RoomRentalSystem.Domain.Exceptions;

namespace RoomRentalSystem.Domain.Entities;

public class ReviewEntity : BaseEntity
{
    public ReviewScoreEntity Score { get; private set; }
    public string Comment { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Guid UserId { get; private set; }
    public UserEntity User { get; private set; }
    public Guid RoomId { get; private set; }
    public RoomEntity Room { get; private set; }

    private ReviewEntity() { }

    public static ReviewEntity Create(
        Guid userId,
        Guid roomId,
        int rating,
        string comment)
    {
        if (userId == Guid.Empty)
        {
            throw new DomainException(nameof(userId));
        }
        if (roomId == Guid.Empty)
        {
            throw new DomainException(nameof(roomId));
        }
        if (string.IsNullOrWhiteSpace(comment))
        {
            throw new DomainException(nameof(comment));
        }

        var score = ReviewScoreEntity.Create(rating);

        return new ReviewEntity
        {
            UserId = userId,
            RoomId = roomId,
            Score = score,
            Comment = comment,
            CreatedAt = DateTime.Now
        };
    }
}