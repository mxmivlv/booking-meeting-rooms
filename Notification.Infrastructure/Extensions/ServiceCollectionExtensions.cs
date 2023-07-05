using Microsoft.Extensions.DependencyInjection;
using Notification.Infrastructure.Connections;
using Notification.Infrastructure.Interfaces.Connections;

namespace Notification.Infrastructure.Extensions;

/// <summary>
/// Расширение для подключения сервисов Notification.Infrastructure
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Подключение сервисов
    /// </summary>
    public static IServiceCollection AddNotificationInfrastructure(this IServiceCollection services)
    {
        // Подключение RabbitMq
        services.AddScoped<IConnectionRabbit, ConnectionRabbit>();
        // Подключение Kafka
        services.AddScoped<IConnectionKafka, ConnectionKafka>();
        // Подключение Telegram
        services.AddScoped<IConnectionTelegram, ConnectionTelegram>();

        return services;
    }
}