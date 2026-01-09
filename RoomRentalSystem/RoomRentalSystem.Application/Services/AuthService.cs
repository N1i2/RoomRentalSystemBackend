using RoomRentalSystem.Application.DTOs;
using RoomRentalSystem.Application.Services.Interfaces;
using RoomRentalSystem.Domain.IRepositories;
using Mapster;

namespace RoomRentalSystem.Application.Services;

public class AuthService(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IJwtTokenGenerator jwtTokenGenerator)
    : IAuthService
{
    public async Task<AuthResponseDto> AuthenticateAsync(LoginUserDto dto)
    {
        var user = await userRepository.GetByEmailAsync(dto.Email);

        if (!passwordHasher.VerifyPassword(dto.Password, user.PasswordHash))
        {
            throw new ApplicationException("Invalid credentials");
        }

        var token = jwtTokenGenerator.GenerateToken(user);

        return new AuthResponseDto { User = user.Adapt<UserDto>(), Token = token};
    }
}