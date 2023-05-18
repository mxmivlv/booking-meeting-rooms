using Application.Features.Models;
using Application.Models.Dto;
using AutoMapper;
using Domain.Interfaces.Infrastructure;
using MediatR;

namespace Application.Features.Queries;

public class GetScheduleSpecificRoomHandler : IRequestHandler<GetScheduleSpecificRoomRequest, MeetingRoomDto>
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
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен</param>
    /// <returns>Расписание комнаты</returns>
    public async Task<MeetingRoomDto> Handle(GetScheduleSpecificRoomRequest request, CancellationToken cancellationToken)
    {
        var meetingRoom = await _repository.GetScheduleAsync(request.Id);
            
        return _mapper.Map<MeetingRoomDto>(meetingRoom);
    }

    #endregion
}