using Infrastructure.Connections.Interfaces;
using Infrastructure.Settings;
using Infrastructure.Settings.RabbitMQ;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Infrastructure.Connections;

public class ConnectionRabbitMQ: IConnectionRabbitMQ
{
    #region Свойства

    public IModel Channel { get; private set; }
    
    public RabbitMQSettings Settings { get; }

    #endregion

    #region Конструктор

    public ConnectionRabbitMQ(IOptions<InfrastructureSettings> settings)
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