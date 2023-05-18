namespace Application.Models.Dto;

public class BookingMeetingRoomDto
{
    #region Свойства

    public DateOnly DateMeeting { get; set; }
    
    public TimeOnly StartTimeMeeting { get; set; }
    
    public TimeOnly EndTimeMeeting { get; set; }

    #endregion
}