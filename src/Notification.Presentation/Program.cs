using Notification.Application.Extensions;
using Notification.Application.Services;
using Notification.Infrastructure.Extensions;
using Notification.Infrastructure.Settings;
using Notification.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Получение экземпляра класса
var infrastructureSettings =  builder.Configuration.GetSection(nameof(NotificationInfrastructureSettings)).Get<NotificationInfrastructureSettings>();

// Использование IOption<>
builder.Services.Configure<NotificationInfrastructureSettings>
    (
        builder.Configuration.GetSection(nameof(NotificationInfrastructureSettings))
    );

builder.Services
    .AddNotificationInfrastructure()
    .AddNotificationPresentation(infrastructureSettings)
    .AddNotificationApplication();

var app = builder.Build();

app.MapGrpcService<GrpcService>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();