using Application.Mediatr.Interfaces.Commands;
using MediatR;

namespace Application.Mediatr.Features.Models;

public class PostBookingReminderNotificationCommand : ICommand<Unit> { }