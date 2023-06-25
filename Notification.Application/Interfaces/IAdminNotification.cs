namespace Notification.Application.Interfaces;

/// <summary>
/// Интерфейс для отправки оповещений в канал администраторам
/// </summary>
public interface IAdminNotification
{
    /// <summary>
    /// Отправка оповещения для администраторов
    /// </summary>
    /// <param name="message">Текст сообщения</param>
    public Task SendMessage(string message);
}