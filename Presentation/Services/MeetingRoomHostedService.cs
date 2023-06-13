using Application.Mediatr.Features.Models;
using MediatR;

namespace Presentation.Services;

public class MeetingRoomHostedService: BackgroundService
{
    #region Поле

    private readonly IServiceProvider _serviceProvider;

    #endregion

    #region Конструктор

    public MeetingRoomHostedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    #endregion

    #region Метод

    /// <summary>
    /// HostedService для разбронирования комнат
    /// </summary>
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                await mediator.Send(new PostUnbookingMeetingRoomCommand());
            }
            await Task.Delay(TimeSpan.FromSeconds(180));
        }
    }

    #endregion
}