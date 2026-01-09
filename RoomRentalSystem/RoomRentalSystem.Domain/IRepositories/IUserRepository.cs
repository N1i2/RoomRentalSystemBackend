using RoomRentalSystem.Domain.Entities;

namespace RoomRentalSystem.Domain.IRepositories;

public interface IUserRepository : IRepository<UserEntity>
{
    Task<UserEntity> GetByEmailAsync(string email);
    Task<bool> ExistsByEmailAsync(string email);
}