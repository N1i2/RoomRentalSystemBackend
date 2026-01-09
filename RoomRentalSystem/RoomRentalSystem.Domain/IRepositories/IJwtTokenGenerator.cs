using RoomRentalSystem.Domain.Entities;

namespace RoomRentalSystem.Domain.IRepositories;

public interface IJwtTokenGenerator
{
    string GenerateToken(UserEntity user);
}