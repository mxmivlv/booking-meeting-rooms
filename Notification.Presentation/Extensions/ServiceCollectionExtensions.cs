using Notification.Presentation.Services;

namespace Notification.Presentation.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Подключение сервисов
    /// </summary>
    public static IServiceCollection AddNotificationPresentation(this IServiceCollection services)
    {
        services.AddHostedService<NotificationHostedService>();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}