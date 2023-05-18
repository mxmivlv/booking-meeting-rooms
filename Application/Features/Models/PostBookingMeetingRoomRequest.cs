using Application.Models.Dto;
using MediatR;

namespace Application.Features.Models;

public class PostBookingMeetingRoomRequest: IRequest<BookingMeetingRoomDto>
{
    #region Свойства

    public Guid Id { get; }
    
    public string DateMeeting { get; }

    public string StartTimeMeeting { get; }
    
    public string EndTimeMeeting { get; }

    #endregion

    #region Конструктор

    public PostBookingMeetingRoomRequest(Guid id, string dateMeeting, string startTimeMeeting, string endTimeMeeting)
    {
        Id = id;
        DateMeeting = dateMeeting;
        StartTimeMeeting = startTimeMeeting;
        EndTimeMeeting = endTimeMeeting;
    }

    #endregion
}