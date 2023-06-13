using Infrastructure.Settings.RabbitMQ;
using RabbitMQ.Client;

namespace Infrastructure.Connections.Interfaces;

public interface IConnectionRabbitMQ
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