using Contracts.Models.Booking;
using MassTransit;
using Notification.Application.Interfaces;

namespace Notification.Application.Services;

/// <summary>
/// Сервис для обработки входящих сообщений
/// </summary>
public class ConsumerBookingService: IConsumer<MessageBooking>
{
    #region Поле

    /// <summary>
    /// Сервис для отправки уведомлений
    /// </summary>
    private readonly INotification _notification;

    #endregion

    #region Конструктор

    public ConsumerBookingService(INotification notification)
    {
        _notification = notification;
    }

    #endregion

    #region Метод

    /// <summary>
    /// Обработка входящих сообщений
    /// </summary>
    /// <param name="context">Модель</param>
    public Task Consume(ConsumeContext<MessageBooking> context)
    {
        var message = context.Message;
        _notification.SendMessage(message.Text, message.IdChat);
        
        return Task.CompletedTask;
    }

    #endregion

    
}