using Confluent.Kafka;
using Domain.Interfaces.Infrastructure;
using Infrastructure.Connections;
using Infrastructure.Interfaces.Connections;
using Infrastructure.Settings;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

/// <summary>
/// Расширение для подключения сервисов Infrastructure
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Подключение сервисов
    /// </summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, InfrastructureSettings settings)
    {
        // База данных
        services.AddDbContext<Context>(options => options.UseNpgsql(settings.ConnectionStringDb));
        services.AddScoped<DbContext, Context>();
        services.AddScoped<IRepository, Repository>();
        // Проверка работы
        services.AddHealthChecks()
            .AddNpgSql(settings.ConnectionStringDb)
            .AddRabbitMQ(new Uri(settings.RabbitMqSettings.ConnectionAmqp))
            .AddRedis(settings.RedisSettings.ConnectionString)
            .AddKafka(new ProducerConfig
            {
                BootstrapServers = settings.KafkaSettings.BootstrapServers,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = settings.KafkaSettings.SaslUsername,
                SaslPassword = settings.KafkaSettings.SaslPassword
            }, settings.KafkaSettings.TopicHealth);

        // Подключение RabbitMq
        services.AddScoped<IConnectionRabbitMq, ConnectionRabbitMq>();
        // Подключение MassTransit
        //services.AddMassTransit(settings);
        // Подключение Redis
        services.AddScoped<IConnectionRedis, ConnectionRedis>();
        // Подключение Kafka
        services.AddScoped<IConnectionKafka, ConnectionKafka>();

        return services;
    }

    /// <summary>
    /// Подключение MassTransit
    /// </summary>
    private static IServiceCollection AddMassTransit(this IServiceCollection services, InfrastructureSettings settings)
    {
        services.AddMassTransit(builder =>
        {
            builder.UsingRabbitMq((context, config) =>
            {
                config.Host
                (
                    settings.RabbitMqSettings.ConnectionString,
                    settings.RabbitMqSettings.VirtualHost, 
                    options =>
                {
                    options.Username(settings.RabbitMqSettings.Login);
                    options.Password(settings.RabbitMqSettings.Password);
                });
                config.ConfigureEndpoints(context);
            });
        });
        return services;
    }
}