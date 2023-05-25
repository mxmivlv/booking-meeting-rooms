namespace Domain.Models;

public class BookingMeetingRoom
{
    #region Свойства

    public Guid Id { get; }

    public DateOnly DateMeeting { get; }
    
    public TimeOnly StartTimeMeeting { get; }
    
    public TimeOnly EndTimeMeeting { get; }
    
    public Guid MeetingRoomId { get; }

    #endregion

    #region Конструктор

    public BookingMeetingRoom(DateOnly dateMeeting, TimeOnly startTimeMeeting, TimeOnly endTimeMeeting, Guid meetingRoomId)
    {
        Id = Guid.NewGuid();
        DateMeeting = dateMeeting;
        StartTimeMeeting = startTimeMeeting;
        EndTimeMeeting = endTimeMeeting;
        MeetingRoomId = meetingRoomId;
    }

    #endregion
}