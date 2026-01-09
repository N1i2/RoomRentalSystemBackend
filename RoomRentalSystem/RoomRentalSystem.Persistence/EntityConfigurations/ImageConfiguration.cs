using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomRentalSystem.Domain.Entities;

namespace RoomRentalSystem.Persistence.EntityConfigurations;

public class ImageConfiguration : IEntityTypeConfiguration<ImageEntity>
{
    private const int NameMaxLength = 100;
    private const int TypeMaxLength = 50;
    private const int MaxImageSize = 10 * 1024 * 1024;

    public void Configure(EntityTypeBuilder<ImageEntity> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.ImageData)
            .IsRequired();

        builder.Property(i => i.Name)
            .IsRequired()
            .HasMaxLength(NameMaxLength);

        builder.Property(i => i.Type)
            .IsRequired()
            .HasMaxLength(TypeMaxLength);

        builder.ToTable(b => b.HasCheckConstraint(
            "CK_Image_Size",
            $"DATALENGTH([ImageData]) <= {MaxImageSize}"));

        builder.ToTable("Images");
    }
}