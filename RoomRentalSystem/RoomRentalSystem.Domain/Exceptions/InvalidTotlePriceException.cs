namespace RoomRentalSystem.Domain.Exceptions;

internal class InvalidTotalPriceException(decimal totalPrice, decimal minTotalPrice)
    : DomainException($"Total price {totalPrice} is less than minimum required {minTotalPrice}");