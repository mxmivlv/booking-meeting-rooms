using Domain.Models;

namespace Domain.Interfaces.Infrastructure;

public interface IRepository
{
    /// <summary>
    /// Бронирование комнаты
    /// </summary>
    /// <param name="id">Id комнаты</param>
    /// <param name="dateMeeting">Дата брони</param>
    /// <param name="startTimeMeeting">Начала брони</param>
    /// <param name="endTimeMeeting">Конец брони</param>
    /// <returns>Данные о бронировании</returns>
    public Task<BookingMeetingRoom> BookingMeetingRoomAsync(Guid id, DateOnly dateMeeting, TimeOnly startTimeMeeting, TimeOnly endTimeMeeting);

    /// <summary>
    /// Получение расписания комнаты
    /// </summary>
    /// <param name="id">Id комнаты</param>
    /// <returns>Комнату с данными</returns>
    public Task<MeetingRoom> GetScheduleAsync(Guid id);

    /// <summary>
    /// Разбронирование комнат (HostedService)
    /// </summary>
    public void UnbookingMeetingRoom(DateOnly currentDateOnly, TimeOnly currentTimeOnly);

    public ICollection<BookingMeetingRoom> GetRoomsForNotification(DateOnly currentDateOnly, TimeOnly currentTimeOnly, TimeOnly maxTimeOnly);
}