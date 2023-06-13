using Application.Mediatr.Features.Models;
using MediatR;

namespace Presentation.Services;

public class NotificationHostedService: BackgroundService
{
    #region Поле

    private readonly IServiceProvider _serviceProvider;

    #endregion
    
    #region Конструктор

    public NotificationHostedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    #endregion

    #region Метод

    /// <summary>
    /// HostedService для отправки напоминаний о бронировании
    /// </summary>
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                await mediator.Send(new PostBookingReminderNotificationCommand());
            }
            await Task.Delay(TimeSpan.FromMinutes(1));
        }
    }

    #endregion
}