using Application.Interfaces;
using Application.Mediatr.Features.Models;
using Application.Mediatr.Interfaces.Commands;
using Contracts.Interface;
using Contracts.Models;
using MediatR;

namespace Application.Mediatr.Features.Commands;

/// <summary>
/// Обработчик разбронирования комнат
/// </summary>
public class PostUnbookingMeetingRoomHandler: ICommandHandler<PostUnbookingMeetingRoomCommand, Unit>
{
    #region Поле

    /// <summary>
    /// Сервис для работы с комнатами
    /// </summary>
    private readonly IRoomService _roomService;
    
    /// <summary>
    /// Сервис для отправки сообщений в шину
    /// </summary>
    private readonly IPublishBusService<IMessage> _publishBusService;

    #endregion

    #region Конструктор

    public PostUnbookingMeetingRoomHandler(IRoomService roomService, IPublishBusService<IMessage> publishBusService)
    {
        _roomService = roomService;
        _publishBusService = publishBusService;
    }

    #endregion

    #region Метод

    /// <summary>
    /// Разбронирование комнат
    /// </summary>
    /// <param name="command">Команда</param>
    /// <param name="cancellationToken">Токен</param>
    public async Task<Unit> Handle(PostUnbookingMeetingRoomCommand command, CancellationToken cancellationToken)
    {
        await _roomService.UnbookingRoomAsync();
        
        // Отправка сообщений в телеграм с администраторами о том, что были разблокированны комнаты.
        // Логику нужно переделать, прежде, чем удалять бронирование, нужно достать все данные и их положить в сообщение
        // Для администраторов. Сейчас просто происходит удаление броней.
        var message = new MessageNotification
        (
            -1001961900437,
            "Комната разбронирована",
            "Дополнительное описание"
        );
        await _publishBusService.SendMessageAsync(message);

        return await Unit.Task;
    }

    #endregion
}