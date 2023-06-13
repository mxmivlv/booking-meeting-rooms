using Application.Mediatr.Features.Models;
using Domain.Models;

namespace Application.Interfaces;

public interface IRoomService
{
    public Task<BookingMeetingRoom> BookingRoomAsync(PostBookingMeetingRoomCommand command);

    public Task UnbookingRoomAsync();
}