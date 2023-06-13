using Microsoft.Extensions.DependencyInjection;
using Notification.Application.Interfaces;
using Notification.Application.Services;
using Notification.Application.RabbitMQ.Interfaces;

namespace Notification.Application.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Подключение сервисов
    /// </summary>
    public static IServiceCollection AddNotificationApplication(this IServiceCollection services)
    {
        // Подключение
        services.AddScoped<INotificationRabbitMQ, NotificationRabbitMqService>();
        services.AddScoped<INotificationService, NotificationTelegramService>();
        
        return services;
    }
}