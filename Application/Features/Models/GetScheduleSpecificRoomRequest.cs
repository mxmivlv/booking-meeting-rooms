using Application.Models.Dto;
using MediatR;

namespace Application.Features.Models;

public class GetScheduleSpecificRoomRequest : IRequest<MeetingRoomDto>
{
    #region Свойство

    public Guid Id { get; }

    #endregion

    #region Конструктор

    public GetScheduleSpecificRoomRequest(Guid id)
    {
        Id = id;
    }

    #endregion
}