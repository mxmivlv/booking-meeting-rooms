using Application.Interfaces;
using Application.Mediatr.Features.Models;
using Application.Mediatr.Interfaces.Commands;
using Application.Models.Dto;
using Application.Services;
using AutoMapper;

namespace Application.Mediatr.Features.Commands;

public class PostBookingMeetingRoomHandler: ICommandHandler<PostBookingMeetingRoomCommand, BookingMeetingRoomDto>
{
    #region Поля

    private readonly IRoomService _roomService;

    private readonly IMapper _mapper;

    #endregion
    
    #region Конструктор

    public PostBookingMeetingRoomHandler(IRoomService roomService, IMapper mapper)
    {
        _roomService = roomService;
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
        var bookingMeetingRoom = await _roomService.BookingRoomAsync(command);

        return _mapper.Map<BookingMeetingRoomDto>(bookingMeetingRoom);
    }

    #endregion
}