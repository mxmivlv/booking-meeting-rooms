﻿using System.Collections;
using Domain.Models;

namespace Application.Interfaces;

/// <summary>
/// Интерфейс для работы с бронированием и разбронированием комнат
/// </summary>
public interface IRoomService
{
    /// <summary>
    /// Бронирование комнаты
    /// </summary>
    /// <param name="idRoom">Id комнаты для бронирования</param>
    /// <param name="dateMeeting">На какую дату забронировать</param>
    /// <param name="startTimeMeeting">Время начала бронирования</param>
    /// <param name="endTimeMeeting">Время конца бронирования</param>
    /// <returns>Данные о бронировании</returns>
    public Task<BookingMeetingRoom> BookingRoomAsync(Guid idRoom, DateOnly dateMeeting, TimeOnly startTimeMeeting, TimeOnly endTimeMeeting);

    /// <summary>
    /// Разбронирование комнаты (HostedService)
    /// </summary>
    public Task UnbookingRoomAsync();
}