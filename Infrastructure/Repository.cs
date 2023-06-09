﻿using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces.Infrastructure;

namespace Infrastructure;

/// <summary>
/// Работа с бд
/// </summary>
public class Repository : IRepository
{
    #region Поле

    /// <summary>
    /// Доступ к бд
    /// </summary>
    private readonly DbContext _context;

    #endregion

    #region Конструктор

    public Repository(DbContext context)
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
        var meetingRoom = await _context.Set<MeetingRoom>()
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
        var meetingRoom = await _context.Set<MeetingRoom>()
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
    public async Task<ICollection<MeetingRoom>> UnbookingMeetingRoomAsync(DateOnly currentDateOnly, TimeOnly currentTimeOnly)
    {
        var meetingRooms = await _context.Set<MeetingRoom>()
            .Include(e => e.BookingMeetingRooms)
            .ToListAsync();

        foreach (var item in meetingRooms)
        {
            // Разбронировать комнату
            item.UnbookingRoom(currentDateOnly, currentTimeOnly);
        }

        return meetingRooms;
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
        var collectionMeetingRoom =_context.Set<BookingMeetingRoom>()
            .Where(e => ((e.DateMeeting == currentDateOnly) 
                         && (e.IsNotification == false)
                         && (e.StartTimeMeeting > currentTimeOnly)
                         && (e.StartTimeMeeting <= maxTimeOnly)))
            .ToList();

        foreach (var item in collectionMeetingRoom)
        {
            // Поставить true о том, что об этом бронировании оповестили
            item.SetTrueNotification();
        }

        return collectionMeetingRoom;
    }

    #endregion
}