﻿using Application.Features.Models;
using Application.Models.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ScheduleMeetingRoomController: ControllerBase
{
    #region Поле

    private readonly IMediator _mediator;

    #endregion
    
    #region Конструктор

    public ScheduleMeetingRoomController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    #region Api - Методы

    /// <summary>
    /// Расписание комнаты
    /// </summary>
    /// <param name="id">Id комнаты</param>
    /// <returns>Расписание комнаты</returns>
    [HttpPost]
    public async Task<MeetingRoomDto> ScheduleSpecificRoomAsync(Guid id)
    {
        return await _mediator.Send(new GetScheduleSpecificRoomRequest(id));
    }

    #endregion
}