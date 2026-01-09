using System.Security.AccessControl;

namespace RoomRentalSystem.Application.DTOs;

public record CreateUserDto
{
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public IEnumerable<Guid> RoleIds { get; set; } =[];
}