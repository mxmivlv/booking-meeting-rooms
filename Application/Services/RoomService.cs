using Application.Interfaces;
using Application.Mediatr.Features.Models;
using Domain.Interfaces.Infrastructure;
using Domain.Models;
using Infrastructure.Connections.Interfaces;

namespace Application.Services;

public class RoomService: IRoomService
{
    #region Поля

    private readonly IRepository _repository;

    private readonly IConnectionRedis _connect;

    #endregion

    #region Конструктор

    public RoomService(IRepository repository, IConnectionRedis connect)
    {
        _repository = repository;
        _connect = connect;
    }

    #endregion

    #region Методы

    /// <summary>
    /// Бронирование комнаты
    /// </summary>
    /// <param name="command">Запрос</param>
    /// <returns>Комнату, которую забронировали</returns>
    public async Task<BookingMeetingRoom> BookingRoomAsync(PostBookingMeetingRoomCommand command)
    {
        // Максимальное время блокировки при сбое
        var expiry = TimeSpan.FromSeconds(30);
        
        // Время блокировки ресурса
        var wait = TimeSpan.FromSeconds(10);
        
        // Попытки получить доступ
        var retry = TimeSpan.FromSeconds(1);
        
        await using (var redLock = await _connect.RedLockFactory.CreateLockAsync(_connect.Settings.ConnectionStringRedis, expiry, wait, retry))
        {
            if (redLock.IsAcquired)
            {
                var tempDateMeeting = DateOnly.Parse(command.DateMeeting);
                var tempStartTimeMeeting = TimeOnly.Parse(command.StartTimeMeeting);
                var tempEndTimeMeeting = TimeOnly.Parse(command.EndTimeMeeting);
            
                var bookingMeetingRoom = 
                    await _repository.BookingMeetingRoomAsync
                        (
                            command.Id, 
                            tempDateMeeting, 
                            tempStartTimeMeeting, 
                            tempEndTimeMeeting
                        );

                return bookingMeetingRoom;
            }
            else
            {
                throw new Exception("Превышено ожидание для бронирования комнаты.");
            }
        }
    }

    /// <summary>
    /// Разбронирование комнаты
    /// </summary>
    public async Task UnbookingRoomAsync()
    {
        // Максимальное время блокировки при сбое
        var expiry = TimeSpan.FromSeconds(30);
        
        // Время блокировки ресурса
        var wait = TimeSpan.FromSeconds(10);
        
        // Попытки получить доступ
        var retry = TimeSpan.FromSeconds(1);
        
        await using (var redLock = await _connect.RedLockFactory.CreateLockAsync(_connect.Settings.ConnectionStringRedis, expiry, wait, retry))
        {
            if (redLock.IsAcquired)
            {
                // Получить текущую дату
                var currentDateOnly = DateOnly.FromDateTime(DateTime.Now);
                // Получить текущее время
                var currentTimeOnly = TimeOnly.FromDateTime(DateTime.Now);
        
                _repository.UnbookingMeetingRoom(currentDateOnly, currentTimeOnly);
            }
            else
            {
                throw new Exception("Превышено ожидание для бронирования комнаты.");
            }
        }
    }

    #endregion
}