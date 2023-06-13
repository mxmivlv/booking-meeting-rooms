using Notification.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Notification.Infrastructure.Connections.Interfaces;
using RabbitMQ.Client;
using RabbitMQSettings = Notification.Infrastructure.Settings.RabbitMQ.RabbitMQSettings;

namespace Notification.Infrastructure.Connections;

public class ConnectionRabbit: IConnectionRabbit
{
    #region Свойства

    public IModel Channel { get; private set; }
    
    public RabbitMQSettings Settings { get; }

    #endregion

    #region Конструктор

    public ConnectionRabbit(IOptions<NotificationInfrastructureSettings> settings)
    {
        Settings = settings.Value.RabbitMqSettings;
        ConnectRabbitMQ();
    }

    #endregion

    #region Метод

    /// <summary>
    /// Подключение к RabbitMQ
    /// </summary>
    private void ConnectRabbitMQ()
    {
        // Создание фабрики (ввод логина и пароля)
        var factory = new ConnectionFactory()
        {
            HostName = Settings.ConnectionStringRabbitMQ, 
            UserName = Settings.LoginRabbitMQ, 
            Password = Settings.PasswordRabbitMQ
        };

        // Создание подключения
        var connect = factory.CreateConnection();
        
        // Создание модели
        Channel = connect.CreateModel();
    }

    #endregion
}