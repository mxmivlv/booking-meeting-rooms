using Application.Interfaces.Queries;
using Application.Models.Dto;

namespace Application.Features.Models;

public class GetScheduleSpecificRoomQueries : IQuery<MeetingRoomDto>
{
    #region Свойства

    public Guid Id { get; }

    #endregion

    #region Конструктор

    public GetScheduleSpecificRoomQueries(Guid id)
    {
        Id = id;
    }

    #endregion
}