using RoomRentalSystem.Domain.Entities;

namespace RoomRentalSystem.Domain.IRepositories;

public interface IRoleRepository : IRepository<RoleEntity>
{
    public Task<RoleEntity> GetByNameAsync(string name);
    public Task<bool> ExistsByNameAsync(string name);
}