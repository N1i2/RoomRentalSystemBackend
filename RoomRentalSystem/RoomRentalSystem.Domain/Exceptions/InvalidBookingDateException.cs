namespace RoomRentalSystem.Domain.Exceptions;

public class InvalidBookingDateException(DateTime startDate, DateTime endDate) : DomainException("Invalid booking date");