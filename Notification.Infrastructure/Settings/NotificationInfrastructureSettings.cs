using Notification.Infrastructure.Settings.RabbitMQ;
using Notification.Infrastructure.Settings.Telegram;

namespace Notification.Infrastructure.Settings;

public class NotificationInfrastructureSettings
{
    /// <summary>
    /// Настройки RabbitMQ
    /// </summary>
    public RabbitMQSettings RabbitMqSettings { get; set; }
    
    /// <summary>
    /// Настройки Telegram
    /// </summary>
    public TelegramSettings TelegramSettings { get; set; }
}