namespace Notification.Infrastructure.Settings.Kafka;

public class KafkaSettings
{
    public string GroupId { get; set; }
    public string BootstrapServers { get; set; }
    public string SaslUsername { get; set; }
    public string SaslPassword { get; set; }
    public string TopicName { get; set; }
}