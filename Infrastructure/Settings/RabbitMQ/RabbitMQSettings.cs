namespace Infrastructure.Settings.RabbitMQ;

public class RabbitMQSettings
{
    /// <summary>
    /// Подключение к RabbitMQ
    /// </summary>
    public string ConnectionStringRabbitMQ { get; set; }
    
    /// <summary>
    /// Пароль RabbitMQ
    /// </summary>
    public string PasswordRabbitMQ { get; set; }
    
    /// <summary>
    /// Логин RabbitMQ
    /// </summary>
    public string LoginRabbitMQ { get; set; }
    
    /// <summary>
    /// Название очереди RabbitMQ
    /// </summary>
    public string QueueName { get; set; }
}