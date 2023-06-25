using Application.AutoMapper.Mapping;
using Application.Interfaces;
using Application.Mediatr.Pipelines;
using Application.Services;
using Contracts.Interface;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

/// <summary>
/// Расширение для подключения сервисов Application
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Подключение сервисов
    /// </summary>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Сервис для работы с бронированием и разбронированием
        services.AddScoped<IRoomService, RoomService>();
        
        // Сервис для отправки оповещения, RebbitMQ
        //services.AddScoped<IPublishBusService<IMessage>, RabbitMqService>();
        // Сервис для отправки оповещения, MassTransit
        services.AddScoped<IPublishBusService<IMessage>, MassTransitRabbitMqService<IMessage>>();
        
        // AutoMapper
        services.AddAutoMapper(typeof(MeetingRoomProfileMapping));
        services.AddAutoMapper(typeof(BookingMeetingRoomProfileMapping));

        // MediatR
        services.AddMediatR(typeof(ServiceCollectionExtensions).Assembly);
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(SavingPipelineBehaviour<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehaviour<,>));
        
        return services;
    }
}