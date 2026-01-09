namespace RoomRentalSystem.Application.DTOs;

public record AuthResponseDto
{
    public UserDto User { get; set; }
    public string Token { get; set; }
}