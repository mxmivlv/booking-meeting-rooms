using System.Reflection;
using Application.Mapping;
using Domain.Interfaces.Infrastructure;
using Infrastructure;
using MediatR;
using Microsoft.OpenApi.Models;
using Presentation.Services;

var builder = WebApplication.CreateBuilder(args);

// Загрузка сборки с MediatR
var assembly = AppDomain.CurrentDomain.Load("Application");

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureSwaggerGen(options =>
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

// MediatR
builder.Services.AddMediatR(assembly);
// AutoMapper
builder.Services.AddAutoMapper(typeof(MeetingRoomProfileMapping));
builder.Services.AddAutoMapper(typeof(BookingMeetingRoomProfileMapping));
// БД
builder.Services.AddScoped<IRepository, Repository>();
// HostedService
builder.Services.AddHostedService<MeetingRoomHostedService>();

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

app.Run();


