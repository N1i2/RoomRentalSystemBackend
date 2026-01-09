using RoomRentalSystem.Application.DTOs;

namespace RoomRentalSystem.Application.Services.Interfaces;

public interface IUserService
{
    Task<UserDto> GetUserByIdAsync(Guid id);
    Task<List<UserDto>> GetAllUsersAsync();
    Task<UserDto> CreateUserAsync(CreateUserDto userDto);
}