using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces.Infrastructure;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using StackExchange.Redis;

namespace Infrastructure;

public class Repository : IRepository
{
    #region Поле

    private readonly Context _context;

    private RedLockFactory _redLockFactory;

    #endregion

    #region Конструктор

    public Repository(Context context)
    {
        _context = context;
        
        ConnectRedis();
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
        // Ресурс который блокируется
        var resource = "local";
        
        // Максимальное время блокировки при сбое
        var expiry = TimeSpan.FromSeconds(30);
        
        // Время блокировки ресурса
        var wait = TimeSpan.FromSeconds(10);
        
        // Попытки получить доступ
        var retry = TimeSpan.FromSeconds(1);
        
        await using (var redLock = await _redLockFactory.CreateLockAsync(resource, expiry, wait, retry))
        {
            if (redLock.IsAcquired)
            {
                var meetingRoom = await _context.MeetingRooms
                                      .Include(e => e.BookingMeetingRooms)
                                      .FirstOrDefaultAsync(e => e.Id == id)
                                  ?? throw new Exception("комнаты с таким Id нет.");
        
                var bookingMeetingRoom = meetingRoom.BookingRoom(dateMeeting, startTimeMeeting, endTimeMeeting);
                await _context.SaveChangesAsync();
                
                return bookingMeetingRoom;
            }
            else
            {
                throw new Exception("Превышено ожидание для бронирования комнаты.");
            }
        }
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
    public async Task UnbookingMeetingRoomAsync(DateOnly currentDateOnly, TimeOnly currentTimeOnly)
    {
        var meetingRooms = _context.MeetingRooms
            .Include(e => e.BookingMeetingRooms)
            .ToList();

        foreach (var item in meetingRooms)
        {
            item.UnbookingRoom(currentDateOnly, currentTimeOnly);
        }
        
        await _context.SaveChangesAsync();
    }
    
    /// <summary>
    /// Подключение к Redis
    /// </summary>
    private void ConnectRedis()
    {
        // Создаем подключение
        var _connection = ConnectionMultiplexer.Connect("localhost");

        // Добавляем подключение
        var multiplexers = new List<RedLockMultiplexer>
        {
            _connection,
        };
        
        _redLockFactory = RedLockFactory.Create(multiplexers);
    }

    #endregion
}