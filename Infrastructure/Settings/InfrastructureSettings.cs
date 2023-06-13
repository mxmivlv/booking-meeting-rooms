using Infrastructure.Settings.RabbitMQ;
using Infrastructure.Settings.Redis;

namespace Infrastructure.Settings;

public class InfrastructureSettings
{
    /// <summary>
    /// Строка подключения к бд
    /// </summary>
    public string ConnectionStringDb { get; set; }

    /// <summary>
    /// Настройки Redis
    /// </summary>
    public RedisSettings RedisSettings { get; set; }
    
    /// <summary>
    /// Настройки RabbitMQ
    /// </summary>
    public RabbitMQSettings RabbitMqSettings { get; set; }
}