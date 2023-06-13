using Microsoft.Extensions.DependencyInjection;
using Notification.Infrastructure.Connections;
using Notification.Infrastructure.Connections.Interfaces;

namespace Notification.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Подключение сервисов
    /// </summary>
    public static IServiceCollection AddNotificationInfrastructure(this IServiceCollection services)
    {
        // Подключение
        services.AddScoped<IConnectionRabbit, ConnectionRabbit>();
        services.AddScoped<IConnectionTelegram, ConnectionTelegram>();
        
        return services;
    }
}