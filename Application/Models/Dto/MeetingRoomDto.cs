namespace Application.Models.Dto;

public class MeetingRoomDto
{
    #region Свойства

    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string? Description { get; set; }
    
    public List<BookingMeetingRoomDto> BookingMeetingRoomsDto { get; set; }
    
    #endregion
}