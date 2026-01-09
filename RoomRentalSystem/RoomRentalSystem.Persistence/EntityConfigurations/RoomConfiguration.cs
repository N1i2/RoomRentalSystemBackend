using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomRentalSystem.Domain.Entities;

namespace RoomRentalSystem.Persistence.EntityConfigurations;

public class RoomConfiguration : IEntityTypeConfiguration<RoomEntity>
{
    private const int TitleMaxLength = 100;
    private const int DescriptionMaxLength = 2000;
    private const int AddressMaxLength = 200;
    private const int AmenitiesMaxLength = 1000;
    private const decimal MinPricePerDay = 1;
    private const double MinArea = 1;
    private const int MinRoomCount = 1;

    public void Configure(EntityTypeBuilder<RoomEntity> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Title)
            .IsRequired()
            .HasMaxLength(TitleMaxLength);

        builder.Property(r => r.PricePerDay)
            .IsRequired()
            .HasColumnType("decimal(18,2)")
            .HasAnnotation("MinValue", MinPricePerDay);

        builder.Property(r => r.Description)
            .IsRequired()
            .HasMaxLength(DescriptionMaxLength);

        builder.Property(r => r.Floor)
            .IsRequired();

        builder.Property(r => r.Area)
            .IsRequired()
            .HasAnnotation("MinValue", MinArea);

        builder.Property(r => r.RoomCount)
            .IsRequired()
            .HasAnnotation("MinValue", MinRoomCount);

        builder.Property(r => r.Address)
            .IsRequired()
            .HasMaxLength(AddressMaxLength);

        builder.Property(r => r.Amenities)
            .HasMaxLength(AmenitiesMaxLength);

        builder.Property(r => r.Status)
            .IsRequired()
            .HasConversion<string>();

        builder.HasOne(r => r.User)
            .WithMany(u => u.Rooms)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Image)
            .WithOne()
            .HasForeignKey<RoomEntity>(r => r.ImageId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(r => r.Bookings)
            .WithOne(b => b.Room)
            .HasForeignKey(b => b.RoomId);

        builder.HasMany(r => r.Reviews)
            .WithOne(r => r.Room)
            .HasForeignKey(r => r.RoomId);

        builder.ToTable("Rooms");
    }
}