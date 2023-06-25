namespace Infrastructure.Settings.RabbitMQ;

/// <summary>
/// Настройки RabbitMq
/// </summary>
public class RabbitMqSettings
{
    /// <summary>
    /// Подключение к RabbitMq
    /// </summary>
    public string ConnectionString { get; set; }
    
    /// <summary>
    /// Наименование провайдера
    /// </summary>
    public string NameProvider { get; set; }
    
    /// <summary>
    /// VirtualHost RabbitMq
    /// </summary>
    public string VirtualHost { get; set; }
    
    /// <summary>
    /// Пароль RabbitMq
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    /// Логин RabbitMq
    /// </summary>
    public string Login { get; set; }
    
    /// <summary>
    /// Название очереди RabbitMq, для администраторов
    /// </summary>
    public string QueueAdmin { get; set; }
    
    /// <summary>
    /// Название очереди RabbitMq, для пользователей
    /// </summary>
    public string QueueUser { get; set; }
}