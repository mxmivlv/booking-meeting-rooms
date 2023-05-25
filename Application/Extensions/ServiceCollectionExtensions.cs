using Application.Mapping;
using Application.Pipelines;
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
        // AutoMapper
        services.AddAutoMapper(typeof(MeetingRoomProfileMapping));
        services.AddAutoMapper(typeof(BookingMeetingRoomProfileMapping));
        
        // MediatR
        services.AddMediatR(typeof(ServiceCollectionExtensions).Assembly);
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehaviour<,>));
        
        return services;
    }
}