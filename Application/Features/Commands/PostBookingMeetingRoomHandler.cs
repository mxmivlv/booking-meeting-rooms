using Application.Features.Models;
using Application.Models.Dto;
using AutoMapper;
using Domain.Interfaces.Infrastructure;
using MediatR;

namespace Application.Features.Commands;

public class PostBookingMeetingRoomHandler: IRequestHandler<PostBookingMeetingRoomRequest, BookingMeetingRoomDto>
{
    #region Поля

    private readonly IRepository _repository;
    
    private readonly IMapper _mapper;

    #endregion
    
    #region Конструктор

    public PostBookingMeetingRoomHandler(IRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    #endregion

    #region Метод
    
    /// <summary>
    /// Метод бронирования комнат
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен</param>
    /// <returns>Комнату, которую забронировали</returns>
    public async Task<BookingMeetingRoomDto> Handle(PostBookingMeetingRoomRequest request, CancellationToken cancellationToken)
    {
        var tempDateMeeting = DateOnly.Parse(request.DateMeeting);
        var tempStartTimeMeeting = TimeOnly.Parse(request.StartTimeMeeting);
        var tempEndTimeMeeting = TimeOnly.Parse(request.EndTimeMeeting);
            
        var bookingMeetingRoom = await _repository.BookingMeetingRoomAsync(request.Id, tempDateMeeting, tempStartTimeMeeting, tempEndTimeMeeting);
            
        return _mapper.Map<BookingMeetingRoomDto>(bookingMeetingRoom);
    }

    #endregion
}