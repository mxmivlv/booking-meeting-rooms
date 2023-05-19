using Application.Features.Models;
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

    #region Методы

    /// <summary>
    /// Метод для разбронирования комнат
    /// </summary>
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await Task.Run( async () =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                    await mediator.Send(new PostUnbookingMeetingRoomRequest());
                }
                await Task.Delay(TimeSpan.FromSeconds(180));
            }
        });
    }

    #endregion
}