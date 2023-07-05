namespace Notification.Application.Interfaces;

/// <summary>
/// Интерфейс для отправки оповещений
/// </summary>
public interface INotification
{
    /// <summary>
    /// Отправка оповещения
    /// </summary>
    /// <param name="message">Текст сообщения</param>
    /// <param name="idChat">Id чата пользователя</param>
    public Task SendMessage(string message, long idChat);
}