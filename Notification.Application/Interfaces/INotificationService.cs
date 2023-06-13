namespace Notification.Application.Interfaces;

public interface INotificationService
{
    /// <summary>
    /// Отправить сообщение с помощью бота
    /// </summary>
    /// <param name="message">Сообщение</param>
    public Task SendMessage(string message);
}