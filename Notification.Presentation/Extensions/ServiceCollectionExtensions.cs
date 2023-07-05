using MassTransit;
using Notification.Application.Services;
using Notification.Infrastructure.Settings;
using Notification.Presentation.Services;

namespace Notification.Presentation.Extensions;

/// <summary>
/// Расширение для подключения сервисов Notification.Presentation
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Подключение сервисов
    /// </summary>
    public static IServiceCollection AddNotificationPresentation(this IServiceCollection services, NotificationInfrastructureSettings settings)
    {
        // Для работы RabbitMq
        services.AddHostedService<NotificationHostedService>();

        // Работа с помощью MassTransit
        //services.AddMassTransit(settings);
        
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    /// <summary>
    /// Подключение MassTransit с RabbitMq
    /// </summary>
    private static IServiceCollection AddMassTransit(this IServiceCollection services, NotificationInfrastructureSettings settings)
    {
        services.AddMassTransit(builder =>
        {
            builder.SetKebabCaseEndpointNameFormatter();
            
            builder.AddConsumer<ConsumerReminderService>();
            builder.AddConsumer<ConsumerBookingService>();
            builder.AddConsumer<ConsumerUnbookingService>();

            builder.UsingRabbitMq((context, config) =>
            {
                config.Host
                (
                    settings.RabbitMqSettings.ConnectionStringRabbitMQ, 
                    settings.RabbitMqSettings.VirtualHost, 
                    options =>
                {
                    options.Username(settings.RabbitMqSettings.LoginRabbitMQ);
                    options.Password(settings.RabbitMqSettings.PasswordRabbitMQ);
                });
                config.ReceiveEndpoint(settings.RabbitMqSettings.Queue, ep =>
                {
                    ep.PrefetchCount = 16;
                    ep.UseMessageRetry(r => r.Interval(2, 100));
                    ep.ConfigureConsumer<ConsumerReminderService>(context);
                    ep.ConfigureConsumer<ConsumerBookingService>(context);
                    ep.ConfigureConsumer<ConsumerUnbookingService>(context);
                });
            });
        });
        return services;
    }
}