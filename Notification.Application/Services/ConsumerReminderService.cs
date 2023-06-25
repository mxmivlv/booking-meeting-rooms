using Contracts.Models.Reminder;
using MassTransit;
using Notification.Application.Interfaces;

namespace Notification.Application.Services;

/// <summary>
/// Сервис для обработки входящих сообщений
/// </summary>
public class ConsumerReminderService: IConsumer<MessageReminder>
{
    #region Поле

    /// <summary>
    /// Сервис для отправки уведомлений
    /// </summary>
    private readonly INotification _notification;

    #endregion

    #region Конструктор

    public ConsumerReminderService(INotification notification)
    {
        _notification = notification;
    }

    #endregion

    #region Метод

    /// <summary>
    /// Обработка входящих сообщений
    /// </summary>
    /// <param name="context">Модель</param>
    public Task Consume(ConsumeContext<MessageReminder> context)
    {
        var message = context.Message;
        _notification.SendMessage(message.Text, message.IdChat);
        
        return Task.CompletedTask;
    }

    #endregion
}