using Application.Features.Models;
using Domain.Interfaces.Infrastructure;
using Domain.Models;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using StackExchange.Redis;

namespace Application.Services;

public class LockingService
{
    #region Поля

    private readonly IRepository _repository;

    private RedLockFactory _redLockFactory;
    
    // Ресурс который блокируется
    private string resource;

    #endregion

    #region Конструктор

    public LockingService(IRepository repository)
    {
        _repository = repository;
        ConnectRedis();
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
        
        await using (var redLock = await _redLockFactory.CreateLockAsync(resource, expiry, wait, retry))
        {
            if (redLock.IsAcquired)
            {
                var tempDateMeeting = DateOnly.Parse(command.DateMeeting);
                var tempStartTimeMeeting = TimeOnly.Parse(command.StartTimeMeeting);
                var tempEndTimeMeeting = TimeOnly.Parse(command.EndTimeMeeting);
            
                var bookingMeetingRoom = 
                    await _repository.BookingMeetingRoomAsync(command.Id, tempDateMeeting, tempStartTimeMeeting, tempEndTimeMeeting);

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
        
        await using (var redLock = await _redLockFactory.CreateLockAsync(resource, expiry, wait, retry))
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
        
        resource = "local";
    }

    #endregion
}