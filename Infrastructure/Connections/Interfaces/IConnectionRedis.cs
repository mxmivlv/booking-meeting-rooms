using Infrastructure.Settings.Redis;
using RedLockNet.SERedis;

namespace Infrastructure.Connections.Interfaces;

public interface IConnectionRedis
{
    /// <summary>
    /// Фабрика для блокировки потоков
    /// </summary>
    public RedLockFactory RedLockFactory { get; }

    /// <summary>
    /// Настройки Redis
    /// </summary>
    public RedisSettings Settings { get; }
}