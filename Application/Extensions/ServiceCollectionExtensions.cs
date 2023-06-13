using Application.AutoMapper.Mapping;
using Application.Interfaces;
using Application.Mediatr.Pipelines;
using Application.RabbitMQ.Interfaces;
using Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Подключение сервисов
    /// </summary>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IRoomService, RoomService>();
        
        // AutoMapper
        services.AddAutoMapper(typeof(MeetingRoomProfileMapping));
        services.AddAutoMapper(typeof(BookingMeetingRoomProfileMapping));
        
        // RebbitMQ
        services.AddScoped<INotificationRabbitMQ, NotificationRabbitMqService>();
        
        // MediatR
        services.AddMediatR(typeof(ServiceCollectionExtensions).Assembly);
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(SavingPipelineBehaviour<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(BookingNotificationPipelineBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehaviour<,>));

        return services;
    }
}