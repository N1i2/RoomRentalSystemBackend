using Microsoft.EntityFrameworkCore;
using RoomRentalSystem.Domain.Entities;
using RoomRentalSystem.Domain.IRepositories;
using RoomRentalSystem.Persistence.Exceptions;

namespace RoomRentalSystem.Persistence.Repositories;

public class UserRepository(RoomRentalSystemDbContext context) : BaseRepository<UserEntity>(context), IUserRepository
{
    public async Task<UserEntity> GetByEmailAsync(string email)
    {
        return await context.Set<UserEntity>()
                   .Include(u => u.Roles) 
                   .FirstOrDefaultAsync(u => u.Email == email)
               ?? throw new PersistenceException($"User with email '{email}' not found");
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await context.Set<UserEntity>()
            .AnyAsync(u => u.Email == email);
    }
}