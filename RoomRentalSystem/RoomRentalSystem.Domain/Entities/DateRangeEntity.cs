using RoomRentalSystem.Domain.Exceptions;

namespace RoomRentalSystem.Domain.Entities;

public record DateRangeEntity
{
    private const int MaxRentalDay = 365;
    private const int MinRentalDay = 1;

    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public int DurationInDays => (EndDate - StartDate).Days;

    private DateRangeEntity() { }

    public static DateRangeEntity Create(DateTime startDate, DateTime endDate)
    {
        if (startDate >= endDate ||
            (endDate - startDate).TotalDays > MaxRentalDay ||
            startDate.Date < DateTime.UtcNow.Date ||
            (endDate - startDate).TotalDays <= MinRentalDay)
        {
            throw new InvalidBookingDateException(startDate, endDate);
        }

        return new DateRangeEntity
        {
            StartDate = startDate,
            EndDate = endDate
        };
    }
}