using Application.Mediatr.Interfaces.Commands;
using Application.Models.Dto;

namespace Application.Mediatr.Features.Models;

public class PostBookingMeetingRoomCommand: ICommand<BookingMeetingRoomDto>
{
    #region Свойства

    public Guid Id { get; }
    
    public string DateMeeting { get; }

    public string StartTimeMeeting { get; }
    
    public string EndTimeMeeting { get; }

    #endregion

    #region Конструктор

    public PostBookingMeetingRoomCommand(Guid id, string dateMeeting, string startTimeMeeting, string endTimeMeeting)
    {
        Id = id;
        DateMeeting = dateMeeting;
        StartTimeMeeting = startTimeMeeting;
        EndTimeMeeting = endTimeMeeting;
    }

    #endregion
}