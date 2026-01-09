using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomRentalSystem.Domain.Entities;

namespace RoomRentalSystem.Persistence.EntityConfigurations;

public class ReviewConfiguration : IEntityTypeConfiguration<ReviewEntity>
{
    private const int CommentMaxLength = 2000;
    private const int MinRating = 1;
    private const int MaxRating = 5;

    public void Configure(EntityTypeBuilder<ReviewEntity> builder)
    {
        builder.HasKey(r => r.Id);

        builder.OwnsOne(
            r => r.Score,
            scoreBuilder =>
            {
                scoreBuilder.Property(s => s.Value)
                    .HasColumnName("Rating")  
                    .IsRequired();
            });

        builder.Property(r => r.Comment)
            .IsRequired()
            .HasMaxLength(CommentMaxLength);

        builder.Property(r => r.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()");

        builder.HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Room)
            .WithMany(r => r.Reviews)
            .HasForeignKey(r => r.RoomId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(b => b.HasCheckConstraint(
            "CK_Review_Rating",
            $"[Rating] >= {MinRating} AND [Rating] <= {MaxRating}"));

        builder.ToTable("Reviews");
    }
}