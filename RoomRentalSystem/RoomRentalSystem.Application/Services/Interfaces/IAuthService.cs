using RoomRentalSystem.Application.DTOs;
using System.Security.Claims;

namespace RoomRentalSystem.Application.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> AuthenticateAsync(LoginUserDto dto);
}