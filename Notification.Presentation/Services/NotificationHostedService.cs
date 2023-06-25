using Notification.Application.Interfaces;

namespace Notification.Presentation.Services;

/// <summary>
/// Отправка напоминаний о бронировании
/// </summary>
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
    /// Метод для отправки уведомлений
    /// </summary>
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                var notification = scope.ServiceProvider.GetRequiredService<IConsumerBus>();
                notification.Listen();
            }
            await Task.Delay(TimeSpan.FromSeconds(10));
        }
    }

    #endregion
}