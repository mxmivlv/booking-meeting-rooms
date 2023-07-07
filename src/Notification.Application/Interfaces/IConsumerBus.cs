namespace Notification.Application.Interfaces;

/// <summary>
/// Интерфейс для получения сообщений из шины
/// </summary>
public interface IConsumerBus
{
    /// <summary>
    /// Получить новые сообщения из шины
    /// </summary>
    public void Listen();
}