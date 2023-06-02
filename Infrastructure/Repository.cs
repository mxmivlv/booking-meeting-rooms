using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces.Infrastructure;
using RedLockNet.SERedis;

namespace Infrastructure;

public class Repository : IRepository
{
    #region Поля

    private readonly Context _context;

    private RedLockFactory _redLockFactory;

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
                              .FirstOrDefaultAsync(e => e.Id == id)
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
                              .FirstOrDefaultAsync(e => e.Id == id)
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
    /// Сохранение данных в бд
    /// </summary>
    public void Save()
    {
        _context.SaveChanges();
    }

    #endregion
}