using Domain.Interfaces.Infrastructure;
using Infrastructure.Connections;
using Infrastructure.Connections.Interfaces;
using Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Подключение сервисов
    /// </summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, InfrastructureSettings settings)
    {
        services.AddDbContext<Context>(options => options.UseNpgsql(settings.ConnectionStringDb));
        services.AddScoped<IRepository, Repository>();

        // Подключение
        services.AddScoped<IConnectionRabbitMQ, ConnectionRabbitMQ>();
        services.AddScoped<IConnectionRedis, ConnectionRedis>();

        return services;
    }
}