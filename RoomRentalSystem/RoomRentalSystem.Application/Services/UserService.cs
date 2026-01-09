using Mapster;
using RoomRentalSystem.Application.DTOs;
using RoomRentalSystem.Application.Services.Interfaces;
using RoomRentalSystem.Domain.Entities;
using RoomRentalSystem.Domain.IRepositories;
using System.Security.Claims;
using ApplicationException = RoomRentalSystem.Application.Exceptions.ApplicationException;

namespace RoomRentalSystem.Application.Services;

public class UserService(
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    IPasswordHasher passwordHasher,
    IAuthService jwtService) : IUserService
{
    public async Task<UserDto> GetUserByIdAsync(Guid id)
    {
        var user = await userRepository.GetByIdAsync(id);
        return user.Adapt<UserDto>();
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var users = await userRepository.GetAllAsync();
        return users.Adapt<List<UserDto>>();
    }

    public async Task<UserDto> CreateUserAsync(CreateUserDto userDto)
    {
        if (await userRepository.ExistsByEmailAsync(userDto.Email))
        {
            throw new ApplicationException("User with this email already exists");
        }

        var guestRole = await roleRepository.GetByNameAsync("Guest")
                        ?? throw new ApplicationException("Default role 'Guest' not found");
        var roles = new List<RoleEntity> { guestRole };
        foreach (var roleId in userDto.RoleIds)
        {
            var role = await roleRepository.GetByIdAsync(roleId);
            if (role == null)
            {
                throw new ApplicationException($"Role with ID '{roleId}' not found");
            }
            roles.Add(role);
        }

        var user = UserEntity.Create(
            userDto.PhoneNumber,
            userDto.Email,
            passwordHasher.HashPassword(userDto.Password),
            roles);

        await userRepository.AddAsync(user);
        return user.Adapt<UserDto>();
    }
}