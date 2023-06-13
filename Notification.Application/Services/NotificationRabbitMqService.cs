using System.Text;
using Notification.Application.Interfaces;
using Notification.Infrastructure.Connections.Interfaces;
using Notification.Application.RabbitMQ.Interfaces;
using RabbitMQ.Client.Events;

namespace Notification.Application.Services;

public class NotificationRabbitMqService: INotificationRabbitMQ
{
    #region Поля
    
    private readonly IConnectionRabbit _connect;

    private readonly INotificationService _notificationService;

    #endregion

    #region Конструктор

    public NotificationRabbitMqService(IConnectionRabbit connect, INotificationService notificationService)
    {
        _connect = connect;
        _notificationService = notificationService;
    }

    #endregion

    #region Метод

    /// <summary>
    /// Получить новые сообщения из шины
    /// </summary>
    public void Listen()
    {
        // Подключиться к каналу
        _connect.Channel.QueueDeclare
        (
            queue: _connect.Settings.QueueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        // Создать слушателя
        var consumer = new EventingBasicConsumer(_connect.Channel);

        // Получить новые сообщения
        consumer.Received += (ch, ea) =>
        {
            // Конвертировать сообщения в строку
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());
            
            // Отправить сообщение в телеграм
            _notificationService.SendMessage(content);

            _connect.Channel.BasicAck(ea.DeliveryTag, false);
        };

        _connect.Channel.BasicConsume
        (
            queue: _connect.Settings.QueueName,
            autoAck: false,
            consumerTag: "",
            noLocal: false,
            exclusive: false,
            arguments: null,
            consumer: consumer
        );
    }

    #endregion
}