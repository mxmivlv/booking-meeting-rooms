using Notification.Infrastructure.Settings.RabbitMQ;
using RabbitMQ.Client;

namespace Notification.Infrastructure.Connections.Interfaces;

public interface IConnectionRabbit
{
    /// <summary>
    /// Канал к которому подключен Rabbit
    /// </summary>
    public IModel Channel { get; }

    /// <summary>
    /// Настройки Rabbit
    /// </summary>
    public RabbitMQSettings Settings { get; }
}