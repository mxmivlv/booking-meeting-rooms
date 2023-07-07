using System.Reflection;
using Application.Extensions;
using HealthChecks.UI.Client;
using Infrastructure.Extensions;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using Presentation.Extensions;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);

// Получение экземпляра класса
var infrastructureSettings =  builder.Configuration.GetSection(nameof(InfrastructureSettings)).Get<InfrastructureSettings>();

var configuration = builder.Configuration;

// Использование IOption<>
builder.Services.Configure<InfrastructureSettings>(builder.Configuration.GetSection(nameof(InfrastructureSettings)));

// Регистрация сервисов
builder.Services
    .AddInfrastructure(infrastructureSettings)
    .AddApplication()
    .AddPresentation(configuration);

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapHealthChecksUI();

app.UseOpenTelemetryPrometheusScrapingEndpoint();

app.Run();