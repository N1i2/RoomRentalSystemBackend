namespace RoomRentalSystem.Domain.Exceptions;

internal class InvalidReviewScoreException(int value, int minValue, int maxValue) : DomainException($"Review score must be between {minValue} and {maxValue}");