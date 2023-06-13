using System.Text;
using Application.RabbitMQ.Interfaces;
using Domain.Interfaces.Infrastructure;
using Infrastructure.Connections.Interfaces;
using RabbitMQ.Client;

namespace Application.Services;

public class NotificationRabbitMqService: INotificationRabbitMQ
{
    #region Поля

    private readonly IConnectionRabbitMQ _connectRabbit;

    //private readonly IConnectionRedis _connectionRedis;

    private readonly IRepository _repository;

    #endregion

    #region Конструктор

    public NotificationRabbitMqService(IConnectionRabbitMQ connectRabbit, /*IConnectionRedis connectionRedis,*/ IRepository repository)
    {
        _connectRabbit = connectRabbit;
        //_connectionRedis = connectionRedis;
        _repository = repository;
    }

    #endregion

    #region Методы
        
    /// <summary>
    /// Оповещение о бронировании комнаты
    /// </summary>
    public void Notification()
    {
        var message = "Вы забронировали комнату.";
        SendMessage(message);
    }
    
    /// <summary>
    /// Оповещение, напоминание о забронированной комнаты
    /// </summary>
    public void BookingReminderNotification()
    {
        /*// Максимальное время блокировки при сбое
        var expiry = TimeSpan.FromSeconds(30);
        
        // Время блокировки ресурса
        var wait = TimeSpan.FromSeconds(10);
        
        // Попытки получить доступ
        var retry = TimeSpan.FromSeconds(1);
        
        await using (var redLock = await _connectionRedis.RedLockFactory.CreateLockAsync(_connectionRedis.Settings.ConnectionStringRedis, expiry, wait, retry))
        {
            if (redLock.IsAcquired)
            {
                var message = "У вас забронированна комната. Не забудьте о бронировании.";
                
                // Получить текущую дату
                var currentDateOnly = DateOnly.FromDateTime(DateTime.Now);
                // Получить текущее время
                var currentTimeOnly = TimeOnly.FromDateTime(DateTime.Now);
                // Максимально время
                var maxTimeOnly = currentTimeOnly.AddHours(1);

                var collectionMeetingRoom = _repository.GetRoomsForNotification(currentDateOnly, currentTimeOnly, maxTimeOnly);

                foreach (var item in collectionMeetingRoom)
                {
                    // Поставить true о том, что об этом бронировании оповестили
                    item.SetTrueNotification();
                    SendMessage($"{message} Дата: {item.DateMeeting}, время: {item.StartTimeMeeting}");
                }
            }
            else
            {
                throw new Exception("Превышено ожидание для бронирования комнаты.");
            }
        }*/


        var message = "У вас забронированна комната. Не забудьте о бронировании.";
                
        // Получить текущую дату
        var currentDateOnly = DateOnly.FromDateTime(DateTime.Now);
        // Получить текущее время
        var currentTimeOnly = TimeOnly.FromDateTime(DateTime.Now);
        // Максимально время
        var maxTimeOnly = currentTimeOnly.AddHours(1);
        
        var collectionMeetingRoom = _repository.GetRoomsForNotification(currentDateOnly, currentTimeOnly, maxTimeOnly);
        
        foreach (var item in collectionMeetingRoom)
        {
            // Поставить true о том, что об этом бронировании оповестили
            item.SetTrueNotification();
            SendMessage($"{message} Дата: {item.DateMeeting}, время: {item.StartTimeMeeting}");
        }
    }

    /// <summary>
    /// Отправка сообщения
    /// </summary>
    /// <param name="message">Сообщение</param>
    private void SendMessage(string message)
    {
        _connectRabbit.Channel.QueueDeclare
        (
            queue: _connectRabbit.Settings.QueueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        // Создание массива байтов из строки
        var body = Encoding.UTF8.GetBytes(message);

        // Отправка сообщения
        _connectRabbit.Channel.BasicPublish
        (
            exchange: "",
            routingKey: _connectRabbit.Settings.QueueName,
            basicProperties: null,
            body: body
        );
    }

    #endregion
}