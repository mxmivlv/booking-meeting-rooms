using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class MeetingRoomConfiguration : IEntityTypeConfiguration<MeetingRoom>
{
    #region Метод

    public void Configure(EntityTypeBuilder<MeetingRoom> builder)
    {
        builder.HasKey(q => q.Id);
        builder.HasIndex(q => q.Name).HasDatabaseName("NameIndex").IsUnique();
        builder.Property(q => q.Description).HasMaxLength(50);
        builder
            .HasMany(q => q.BookingMeetingRooms)
            .WithOne()
            .HasForeignKey(q=>q.MeetingRoomId);
    }

    #endregion
}