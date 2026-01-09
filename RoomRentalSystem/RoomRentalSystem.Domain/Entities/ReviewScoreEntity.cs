using RoomRentalSystem.Domain.Exceptions;

namespace RoomRentalSystem.Domain.Entities;

public record ReviewScoreEntity
{
    private const int MaxValue = 5;
    private const int MinValue = 1;

    public int Value { get; private set; }

    private ReviewScoreEntity() {}

    public static ReviewScoreEntity Create(int value)
    {
        if (value is < MinValue or > MaxValue)
        {
            throw new InvalidReviewScoreException(
                value,
                MinValue,
                MaxValue);
        }

        return new ReviewScoreEntity{ Value = value};
    }
}