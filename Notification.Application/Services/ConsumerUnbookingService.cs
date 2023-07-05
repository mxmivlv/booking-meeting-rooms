using Contracts.Models.Unbooking;
using MassTransit;
using Notification.Application.Interfaces;

namespace Notification.Application.Services;

/// <summary>
/// Сервис для обработки входящих сообщений
/// </summary>
public class ConsumerUnbookingService: IConsumer<MessageUnbooking>
{
    #region Поле

    /// <summary>
    /// Сервис для отправки уведомлений пользователям
    /// </summary>
    private readonly INotification _notification;

    #endregion

    #region Конструктор

    public ConsumerUnbookingService(INotification notification)
    {
        _notification = notification;
    }

    #endregion

    #region Метод

    /// <summary>
    /// Обработка входящих сообщений
    /// </summary>
    /// <param name="context">Модель</param>
    public Task Consume(ConsumeContext<MessageUnbooking> context)
    {
        var message = context.Message;
        _notification.SendMessage(message.Text, message.IdChat);

        return Task.CompletedTask;
    }

    #endregion
}