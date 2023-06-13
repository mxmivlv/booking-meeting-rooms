using Application.Mediatr.Features.Models;
using Application.Mediatr.Interfaces.Commands;
using Application.RabbitMQ.Interfaces;
using MediatR;
using Serilog;

namespace Application.Mediatr.Features.Commands;

public class PostBookingReminderNotificationHandler: ICommandHandler<PostBookingReminderNotificationCommand, Unit>
{
    #region Поле

    private readonly INotificationRabbitMQ _notificationRabbitMq;

    #endregion
    
    #region Конструктор

    public PostBookingReminderNotificationHandler(INotificationRabbitMQ notificationRabbitMq)
    {
        _notificationRabbitMq = notificationRabbitMq;
    }

    #endregion

    #region Метод

    /// <summary>
    /// Отправка оповещения в телеграм о том, что клиент забронировал комнату
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен</param>
    public Task<Unit> Handle(PostBookingReminderNotificationCommand request, CancellationToken cancellationToken)
    {
        _notificationRabbitMq.BookingReminderNotification();

        return Unit.Task;
    }

    #endregion
}