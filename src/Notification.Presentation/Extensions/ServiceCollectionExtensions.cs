using Confluent.Kafka;
using Contracts.Models;
using MassTransit;
using Notification.Application.Services;
using Notification.Infrastructure.Settings;

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
        // Если реализация шин базовая, то подключается hosted service
        // если реализация шин с помошью MassTransit, то подключается MassTransit
        //services.AddHostedService<NotificationHostedService>();

        // Работа с помощью MassTransit
        //services.AddMassTransitRabbitMq(settings);
        services.AddMassTransitKafka(settings);
        
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    /// <summary>
    /// Подключение MassTransit RabbitMq
    /// </summary>
    private static IServiceCollection AddMassTransitRabbitMq(this IServiceCollection services, NotificationInfrastructureSettings settings)
    {
        services.AddMassTransit(builder =>
        {
            builder.SetKebabCaseEndpointNameFormatter();
            
            builder.AddConsumer<MessageNotificationService>();

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
                    ep.ConfigureConsumer<MessageNotificationService>(context);
                });
            });
        });
        return services;
    }
    
    private static IServiceCollection AddMassTransitKafka(this IServiceCollection services, NotificationInfrastructureSettings settings)
    {
        services.AddMassTransit(builder =>
        {
            builder.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
            
            builder.AddRider(rider =>
            {
                rider.AddConsumer<MessageNotificationService>();

                rider.UsingKafka((context, host) =>
                {
                    host.Host(settings.KafkaSettings.BootstrapServers, h =>
                    {
                        /*h.UseSasl(sasl =>
                        {
                            sasl.Username = settings.KafkaSettings.SaslUsername;
                            sasl.Password = settings.KafkaSettings.SaslPassword;
                            sasl.Mechanism = SaslMechanism.Plain;
                        });*/
                    });
                    //host.SecurityProtocol = SecurityProtocol.SaslSsl;
                    
                    host.TopicEndpoint<MessageNotification>(
                        settings.KafkaSettings.TopicName, 
                        settings.KafkaSettings.GroupId, 
                        ep =>
                        {
                            ep.ConfigureConsumer<MessageNotificationService>(context);
                        });
                });
            });
            
        });
        return services;
    }
}