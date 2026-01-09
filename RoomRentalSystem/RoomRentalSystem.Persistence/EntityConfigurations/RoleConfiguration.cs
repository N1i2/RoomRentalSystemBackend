using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomRentalSystem.Domain.Entities;

namespace RoomRentalSystem.Persistence.EntityConfigurations;

public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    private const int NameMaxLength = 50;

    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(NameMaxLength)
            .HasConversion(
                v => v,
                v => v.Trim());

        builder.HasMany(r => r.Users)
            .WithMany(u => u.Roles)
            .UsingEntity(j => j.ToTable("UserRoles"));

        builder.ToTable("Roles");
    }
}