using System.Text;
using Contracts.Models.Base;
using Newtonsoft.Json;
using Notification.Application.Interfaces;
using Notification.Infrastructure.Interfaces.Connections;
using RabbitMQ.Client.Events;

namespace Notification.Application.Services;

/// <summary>
/// Сервис для получения сообщений из шины
/// </summary>
public class RabbitMqService: IConsumerBus
{
    #region Поля
    
    /// <summary>
    /// Подключение к шине
    /// </summary>
    private readonly IConnectionRabbit _connect;

    /// <summary>
    /// Отправка уведомлений для пользователей
    /// </summary>
    private readonly INotification _notification;

    /// <summary>
    /// Отправка уведомлений администраторам
    /// </summary>
    private readonly IAdminNotification _adminNotification;

    #endregion

    #region Конструктор

    public RabbitMqService(IConnectionRabbit connect, INotification notification, IAdminNotification adminNotification)
    {
        _connect = connect;
        _notification = notification;
        _adminNotification = adminNotification;
    }

    #endregion

    #region Метод

    /// <summary>
    /// Получить новые сообщения из шины
    /// </summary>
    public void Listen()
    {
        ListenUserBus();
        ListenAdminBus();
    }
    
    /// <summary>
    /// Получить сообщения из шины пользователей
    /// </summary>
    private void ListenUserBus()
    {
        // Подключиться к каналу пользователей
        _connect.Channel.QueueDeclare
        (
            queue: _connect.Settings.QueueUser,
            durable: true,
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

            var message = JsonConvert.DeserializeObject<BaseMessage>(content);
            
            // Отправить сообщение в телеграм
            _notification.SendMessage(message.Text, message.IdChat);

            _connect.Channel.BasicAck(ea.DeliveryTag, false);
        };

        _connect.Channel.BasicConsume
        (
            queue: _connect.Settings.QueueUser,
            autoAck: false,
            consumerTag: "",
            noLocal: false,
            exclusive: false,
            arguments: null,
            consumer: consumer
        );
    }
    
    /// <summary>
    /// Получить сообщения из шины администраторов
    /// </summary>
    private void ListenAdminBus()
    {
        // Подключиться к каналу с администраторами
        _connect.ChannelAdmin.QueueDeclare
        (
            queue: _connect.Settings.QueueAdmin,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        // Создать слушателя
        var consumerAdmin = new EventingBasicConsumer(_connect.ChannelAdmin);

        // Получить новые сообщения
        consumerAdmin.Received += (ch, ea) =>
        {
            // Конвертировать сообщения в строку
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());

            var message = JsonConvert.DeserializeObject<BaseMessage>(content);
            
            // Отправить сообщение в телеграм
            _adminNotification.SendMessage(message.Text);

            _connect.ChannelAdmin.BasicAck(ea.DeliveryTag, false);
        };

        _connect.ChannelAdmin.BasicConsume
        (
            queue: _connect.Settings.QueueAdmin,
            autoAck: false,
            consumerTag: "",
            noLocal: false,
            exclusive: false,
            arguments: null,
            consumer: consumerAdmin
        );
    }

    #endregion
}