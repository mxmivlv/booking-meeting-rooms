using Application.Mediatr.Features.Models;
using Application.RabbitMQ.Interfaces;
using MediatR;

namespace Application.Mediatr.Pipelines;

public class BookingNotificationPipelineBehavior<TIn, TOut> : IPipelineBehavior<TIn, TOut> where TIn : IRequest<TOut>
{
    #region Поле

    private readonly INotificationRabbitMQ _notificationRabbitMq;

    #endregion

    #region Конструктор

    public BookingNotificationPipelineBehavior(INotificationRabbitMQ notificationRabbitMq)
    {
        _notificationRabbitMq = notificationRabbitMq;
    }

    #endregion

    #region Метод

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request">Запрос, который пришел</param>
    /// <param name="next">Метод, который должен выполниться</param>
    /// <param name="cancellationToken">Токен</param>
    /// <returns>Возвращаемый тип, метода, который должен выполниться</returns>
    public async Task<TOut> Handle(TIn request, RequestHandlerDelegate<TOut> next, CancellationToken cancellationToken)
    {
        var result = await next();
        
        if (request is PostBookingMeetingRoomCommand)
        {
            _notificationRabbitMq.Notification();
            //Log.Information("Оповещение о бронировании комнаты отправленно.");
        }

        return result;
    }

    #endregion
}