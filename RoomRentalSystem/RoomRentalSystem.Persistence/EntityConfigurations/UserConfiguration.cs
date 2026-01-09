using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomRentalSystem.Domain.Entities;
using RoomRentalSystem.Persistence.Converters;

namespace RoomRentalSystem.Persistence.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    private const int PhoneNumberMaxLength = 20;
    private const int EmailMaxLength = 100;
    private const int PasswordHashMaxLength = 256;

    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.PhoneNumber)
            .IsRequired()
            .HasMaxLength(PhoneNumberMaxLength)
            .HasConversion<PhoneNumberConverter>()
            .HasConversion(
                v => v,
                v => v.Trim());

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(EmailMaxLength)
            .HasConversion<EmailConverter>()
            .HasConversion(
                v => v,
                v => v.Trim().ToLower());

        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(PasswordHashMaxLength);

        builder.HasOne(u => u.Image)
            .WithOne()
            .HasForeignKey<UserEntity>(u => u.ImageId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity(j => j.ToTable("UserRoles"));

        builder.HasMany(u => u.Rooms)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId);

        builder.HasMany(u => u.Bookings)
            .WithOne(b => b.User)
            .HasForeignKey(b => b.UserId);

        builder.ToTable("Users");
    }
}