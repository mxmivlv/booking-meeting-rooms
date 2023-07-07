using Confluent.Kafka;
using Notification.Infrastructure.Settings.Kafka;

namespace Notification.Infrastructure.Interfaces.Connections;

public interface IConnectionKafka
{
    /// <summary>
    /// Настройки Kafka
    /// </summary>
    public KafkaSettings Settings { get; }

    public IConsumer<Null, string> Consumer { get; }
}