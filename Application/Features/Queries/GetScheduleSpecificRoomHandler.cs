using Application.Features.Models;
using Application.Interfaces.Queries;
using Application.Models.Dto;
using AutoMapper;
using Domain.Interfaces.Infrastructure;

namespace Application.Features.Queries;

public class GetScheduleSpecificRoomHandler : IQueryHandler<GetScheduleSpecificRoomQueries, MeetingRoomDto>
{
    #region Поля

    private readonly IRepository _repository;

    private readonly IMapper _mapper;

    #endregion

    #region Конструктор

    public GetScheduleSpecificRoomHandler(IRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    #endregion

    #region Метод
    
    /// <summary>
    /// Получение расписания у конкретной комнаты
    /// </summary>
    /// <param name="queries">Запрос</param>
    /// <param name="cancellationToken">Токен</param>
    /// <returns>Расписание комнаты</returns>
    public async Task<MeetingRoomDto> Handle(GetScheduleSpecificRoomQueries queries, CancellationToken cancellationToken)
    {
        var meetingRoom = await _repository.GetScheduleAsync(queries.Id);
            
        return _mapper.Map<MeetingRoomDto>(meetingRoom);
    }

    #endregion
}