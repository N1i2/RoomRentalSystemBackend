using Microsoft.EntityFrameworkCore;
using RoomRentalSystem.Domain.Constants;
using RoomRentalSystem.Domain.Entities;

namespace RoomRentalSystem.Persistence;

public static class DbSeeder
{
    public const string AdminId = "11111111-1111-1111-1111-111111111111";
    public const string GuestId = "22222222-2222-2222-2222-222222222222";
    public const string OwnerId = "33333333-3333-3333-3333-333333333333";

    public static async Task SeedRolesAsync(RoomRentalSystemDbContext context)
    {
        if (await context.Roles.AnyAsync())
        {
            return;
        }

        var roles = new List<RoleEntity>
        {
            RoleEntity.Create(Guid.Parse(AdminId), RoleNames.Admin),
            RoleEntity.Create(Guid.Parse(GuestId), RoleNames.Guest),
            RoleEntity.Create(Guid.Parse(OwnerId), RoleNames.Owner)
        };

        await context.Roles.AddRangeAsync(roles);
        await context.SaveChangesAsync();
    }
}