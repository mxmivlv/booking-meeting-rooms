using Application.Features.Models;
using Application.Interfaces.Commands;
using Application.Models.Dto;
using Application.Services;
using AutoMapper;

namespace Application.Features.Commands;

public class PostBookingMeetingRoomHandler: ICommandHandler<PostBookingMeetingRoomCommand, BookingMeetingRoomDto>
{
    #region Поля

    private readonly LockingService _lockingService;
    
    private readonly IMapper _mapper;

    #endregion
    
    #region Конструктор

    public PostBookingMeetingRoomHandler(LockingService lockingService, IMapper mapper)
    {
        _lockingService = lockingService;
        _mapper = mapper;
    }

    #endregion

    #region Метод
    
    /// <summary>
    /// Метод бронирования комнат
    /// </summary>
    /// <param name="command">Запрос</param>
    /// <param name="cancellationToken">Токен</param>
    /// <returns>Комнату, которую забронировали</returns>
    public async Task<BookingMeetingRoomDto> Handle(PostBookingMeetingRoomCommand command, CancellationToken cancellationToken)
    {
        var bookingMeetingRoom = await _lockingService.BookingRoomAsync(command);
        
        return _mapper.Map<BookingMeetingRoomDto>(bookingMeetingRoom);
    }

    #endregion
}