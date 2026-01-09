using Microsoft.EntityFrameworkCore;
using RoomRentalSystem.Domain.Entities;
using RoomRentalSystem.Domain.IRepositories;
using RoomRentalSystem.Persistence.Exceptions;

namespace RoomRentalSystem.Persistence.Repositories;

public class RoleRepository(RoomRentalSystemDbContext context) : BaseRepository<RoleEntity>(context), IRoleRepository
{
    public async Task<RoleEntity> GetByNameAsync(string name)
    {
        return await context.Set<RoleEntity>()
                   .FirstOrDefaultAsync(r => r.Name == name)
               ?? throw new PersistenceException($"Role with name '{name}' not found");
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await context.Set<RoleEntity>()
            .AnyAsync(r => r.Name == name);
    }
}