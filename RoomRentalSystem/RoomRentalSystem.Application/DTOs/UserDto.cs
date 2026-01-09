namespace RoomRentalSystem.Application.DTOs;

public record UserDto
{
    public Guid Id { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public IEnumerable<RoleDto> Roles { get; set; } =[];
}