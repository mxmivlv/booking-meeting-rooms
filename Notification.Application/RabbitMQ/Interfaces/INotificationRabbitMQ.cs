namespace Notification.Application.RabbitMQ.Interfaces;

public interface INotificationRabbitMQ
{
    /// <summary>
    /// Получить новые сообщения из шины
    /// </summary>
    public void Listen();
}