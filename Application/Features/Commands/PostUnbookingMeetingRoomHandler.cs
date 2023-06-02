using Application.Features.Models;
using Application.Interfaces.Commands;
using Application.Services;
using MediatR;

namespace Application.Features.Commands;

public class PostUnbookingMeetingRoomHandler: ICommandHandler<PostUnbookingMeetingRoomCommand, Unit>
{
    #region Поле

    private readonly LockingService _lockingService;

    #endregion

    #region Конструктор

    public PostUnbookingMeetingRoomHandler(LockingService lockingService)
    {
        _lockingService = lockingService;
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
        await _lockingService.UnbookingRoomAsync();

        return await Unit.Task;
    }

    #endregion
}