using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class BookingMeetingRoomConfiguration : IEntityTypeConfiguration<BookingMeetingRoom>
{
    #region Метод

    public void Configure(EntityTypeBuilder<BookingMeetingRoom> builder)
    {
        builder.HasKey(q => q.Id);
        builder.Property(q => q.Id).ValueGeneratedNever();
        builder.Property(q => q.DateMeeting).IsRequired();
        builder.Property(q => q.StartTimeMeeting).IsRequired();
        builder.Property(q => q.EndTimeMeeting).IsRequired();
        builder.Property(q => q.MeetingRoomId).IsRequired();
    }

    #endregion
}