using Domain.Models;

namespace Domain.Interfaces.Infrastructure;

/// <summary>
/// Репозиторий
/// </summary>
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
    public Task<ICollection<MeetingRoom>> UnbookingMeetingRoomAsync(DateOnly currentDateOnly, TimeOnly currentTimeOnly);

    /// <summary>
    /// Получения комнат для отправки оповещения
    /// </summary>
    /// <param name="currentDateOnly">Текущая дата</param>
    /// <param name="currentTimeOnly">Текущее время</param>
    /// <param name="maxTimeOnly">Максимальное время для поиска броней</param>
    /// <returns>Коллекцию бронирований</returns>
    public ICollection<BookingMeetingRoom> GetRoomsForNotification(DateOnly currentDateOnly, TimeOnly currentTimeOnly, TimeOnly maxTimeOnly);
}