using System.Reflection;
using Microsoft.OpenApi.Models;
using Presentation.Filters.Exceptions;
using Presentation.Services;
using Serilog;
using Serilog.Events;

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
        
        Log.Logger = new LoggerConfiguration()
            // минимальный уровень логирования - Debug
            .MinimumLevel.Debug()
            // Скрываем логи с уровнем ниже Warning для пространства имен Microsoft.AspNetCore
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
            // расширяем логируемые данные с помощью LogContext
            .Enrich.FromLogContext()
            // пишем логи в консоль с использованием шаблона
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3} {SourceContext}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();

        return services;
    }
}