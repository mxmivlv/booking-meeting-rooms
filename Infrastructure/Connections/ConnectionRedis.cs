using Infrastructure.Connections.Interfaces;
using Infrastructure.Settings;
using Infrastructure.Settings.Redis;
using Microsoft.Extensions.Options;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using StackExchange.Redis;

namespace Infrastructure.Connections;

public class ConnectionRedis: IConnectionRedis
{
    #region Свойства

    public RedLockFactory RedLockFactory { get; private set; }

    public RedisSettings Settings { get; }

    #endregion

    #region Конструктор

    public ConnectionRedis(IOptions<InfrastructureSettings> settings)
    {
        Settings = settings.Value.RedisSettings;
        ConnectRedis();
    }

    #endregion

    #region Метод

    /// <summary>
    /// Подключение к Redis
    /// </summary>
    private void ConnectRedis()
    {
        // Создаем подключение
        var _connection = ConnectionMultiplexer.Connect(Settings.ConnectionStringRedis);

        // Добавляем подключение
        var multiplexers = new List<RedLockMultiplexer>
        {
            _connection,
        };
        
        RedLockFactory = RedLockFactory.Create(multiplexers);
    }

    #endregion
}