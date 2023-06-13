namespace Domain.Models;

public class BookingMeetingRoom
{
    #region Свойства

    public Guid IdBooking { get; }

    public DateOnly DateMeeting { get; }
    
    public TimeOnly StartTimeMeeting { get; }
    
    public TimeOnly EndTimeMeeting { get; }
    
    public bool IsNotification { get; private set; }
    
    public Guid MeetingRoomId { get; private set; }

    #endregion

    #region Конструктор
    
    private BookingMeetingRoom() { }

    public BookingMeetingRoom(DateOnly dateMeeting, TimeOnly startTimeMeeting, TimeOnly endTimeMeeting, Guid meetingRoomId)
    {
        IdBooking = Guid.NewGuid();
        DateMeeting = dateMeeting;
        StartTimeMeeting = startTimeMeeting;
        EndTimeMeeting = endTimeMeeting;
        IsNotification = false;
        MeetingRoomId = meetingRoomId;
    }

    #endregion

    #region Метод

    /// <summary>
    /// Установить, что оповещение было отправленно о текущем бронировании
    /// </summary>
    public void SetTrueNotification()
    {
        IsNotification = true;
    }

    #endregion
}