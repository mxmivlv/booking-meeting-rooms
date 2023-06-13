using System.Reflection;
using Microsoft.OpenApi.Models;
using Presentation.Filters.Exceptions;
using Presentation.Services;

namespace Presentation.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Подключение сервисов
    /// </summary>
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers(config => 
        {
            config.Filters.Add(new ExceptionFilter());
        });
        
        services.AddHostedService<MeetingRoomHostedService>();
        services.AddHostedService<NotificationHostedService>();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.ConfigureSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Бронирование переговорных комнат",
                Description = "Проект ASP.NET Core Web API",
            });
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        return services;
    }
}