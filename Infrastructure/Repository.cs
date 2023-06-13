using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces.Infrastructure;

namespace Infrastructure;

public class Repository : IRepository
{
    #region Поле

    private readonly Context _context;

    #endregion

    #region Конструктор

    public Repository(Context context)
    {
        _context = context;
    }

    #endregion

    #region методы

    /// <summary>
    /// Бронирование комнаты
    /// </summary>
    /// <param name="id">Id комнаты</param>
    /// <param name="dateMeeting">Дата бронирования</param>
    /// <param name="startTimeMeeting">Время начала брони</param>
    /// <param name="endTimeMeeting">Время конца брони</param>
    /// <returns>Комнату с данными</returns>
    public async Task<BookingMeetingRoom> BookingMeetingRoomAsync(Guid id, DateOnly dateMeeting, TimeOnly startTimeMeeting, TimeOnly endTimeMeeting)
    {
        var meetingRoom = await _context.MeetingRooms
                              .Include(e => e.BookingMeetingRooms)
                              .FirstOrDefaultAsync(e => e.IdRoom == id)
                          ?? throw new Exception("комнаты с таким Id нет.");
        
        var bookingMeetingRoom = meetingRoom.BookingRoom(dateMeeting, startTimeMeeting, endTimeMeeting);

        return bookingMeetingRoom;
    }

    /// <summary>
    /// Получение расписание комнаты
    /// </summary>
    /// <param name="id">Id комнаты</param>
    /// <returns>Расписание комнаты</returns>
    public async Task<MeetingRoom> GetScheduleAsync(Guid id)
    {
        var meetingRoom = await _context.MeetingRooms
                              .Include(e => e.BookingMeetingRooms
                                  .OrderBy(e => e.DateMeeting)
                                  .ThenBy(e => e.StartTimeMeeting))
                              .FirstOrDefaultAsync(e => e.IdRoom == id)
                          ?? throw new Exception("комнаты с таким Id нет.");

        return meetingRoom;
    }

    /// <summary>
    /// Разбронирование комнаты
    /// </summary>
    public void UnbookingMeetingRoom(DateOnly currentDateOnly, TimeOnly currentTimeOnly)
    {
        var meetingRooms = _context.MeetingRooms
            .Include(e => e.BookingMeetingRooms)
            .ToList();

        foreach (var item in meetingRooms)
        {
            item.UnbookingRoom(currentDateOnly, currentTimeOnly);
        }
    }

    /// <summary>
    /// Получение бронирований для оповещения
    /// </summary>
    /// <param name="currentDateOnly">Текущая дата</param>
    /// <param name="currentTimeOnly">Текущее время</param>
    /// <param name="maxTimeOnly">Сдвиг по времени, чтоб был диапазон(10:00 - 11:00)</param>
    /// <returns>Коллекцию бронирований</returns>
    public ICollection<BookingMeetingRoom> GetRoomsForNotification(DateOnly currentDateOnly, TimeOnly currentTimeOnly, TimeOnly maxTimeOnly)
    {
        var collectionMeetingRoom =_context.BookingMeetingRooms
            .Where(e => ((e.DateMeeting == currentDateOnly) 
                         && (e.IsNotification == false)
                         && (e.StartTimeMeeting > currentTimeOnly)
                         && (e.StartTimeMeeting <= maxTimeOnly)))
            .ToList();

        return collectionMeetingRoom;
    }

    #endregion
}