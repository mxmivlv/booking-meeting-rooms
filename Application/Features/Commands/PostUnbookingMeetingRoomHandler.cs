﻿using Application.Features.Models;
using Domain.Interfaces.Infrastructure;
using MediatR;

namespace Application.Features.Commands;

public class PostUnbookingMeetingRoomHandler: IRequestHandler<PostUnbookingMeetingRoomRequest, bool>
{
    #region Поле

    private readonly IRepository _repository;

    #endregion

    #region Конструктор

    public PostUnbookingMeetingRoomHandler(IRepository repository)
    {
        _repository = repository;
    }

    #endregion

    #region Метод

    /// <summary>
    /// Метод разбронирования комнат
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен</param>
    /// <returns>true</returns>
    /// <returns>true</returns>
    public async Task<bool> Handle(PostUnbookingMeetingRoomRequest request, CancellationToken cancellationToken)
    {
        // Получить текущую дату
        var currentDateOnly = DateOnly.FromDateTime(DateTime.Now);
        // Получить текущее время
        var currentTimeOnly = TimeOnly.FromDateTime(DateTime.Now);
        
        await _repository.UnbookingMeetingRoomAsync(currentDateOnly, currentTimeOnly);

        return true;
    }

    #endregion
}