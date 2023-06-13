using Application.Interfaces;
using Application.Mediatr.Features.Models;
using Application.Mediatr.Interfaces.Commands;
using MediatR;

namespace Application.Mediatr.Features.Commands;

public class PostUnbookingMeetingRoomHandler: ICommandHandler<PostUnbookingMeetingRoomCommand, Unit>
{
    #region Поле

    private readonly IRoomService _roomService;

    #endregion

    #region Конструктор

    public PostUnbookingMeetingRoomHandler(IRoomService roomService)
    {
        _roomService = roomService;
    }

    #endregion

    #region Метод

    /// <summary>
    /// Метод разбронирования комнат
    /// </summary>
    /// <param name="command">Запрос</param>
    /// <param name="cancellationToken">Токен</param>
    public async Task<Unit> Handle(PostUnbookingMeetingRoomCommand command, CancellationToken cancellationToken)
    {
        await _roomService.UnbookingRoomAsync();

        return await Unit.Task;
    }

    #endregion
}