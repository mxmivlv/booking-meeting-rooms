using Application.Models.Dto;
using AutoMapper;
using Domain.Models;

namespace Application.Mapping;

public class BookingMeetingRoomProfileMapping: Profile
{
    public BookingMeetingRoomProfileMapping()
    {
        CreateMap<BookingMeetingRoom, BookingMeetingRoomDto>()
            .ForMember(dto => dto.DateMeeting, m => m.MapFrom(e => e.DateMeeting))
            .ForMember(dto => dto.StartTimeMeeting, m => m.MapFrom(e => e.StartTimeMeeting))
            .ForMember(dto => dto.EndTimeMeeting, m => m.MapFrom(e => e.EndTimeMeeting));
    }
}