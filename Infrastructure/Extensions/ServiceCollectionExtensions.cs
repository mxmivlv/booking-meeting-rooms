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
        services.AddDbContext<Context>(options => options.UseNpgsql(settings.ConnectionStringDb));

        services.AddScoped<DbContext, Context>();
        services.AddScoped<IRepository, Repository>();

        // Подключение RabbitMq
        //services.AddScoped<IConnectionRabbitMq, ConnectionRabbitMq>();
        // Подключение MassTransit
        services.AddMassTransit(settings);
        // Подключение Redis
        services.AddScoped<IConnectionRedis, ConnectionRedis>();

        return services;
    }

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