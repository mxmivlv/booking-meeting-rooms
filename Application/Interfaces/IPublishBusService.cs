using Contracts.Interface;
using Contracts.Models.Base;

namespace Application.Interfaces;

/// <summary>
/// Интерфейс для отправки сообщений в шину
/// </summary>
public interface IPublishBusService<T> where T: IMessage
{
    /// <summary>
    /// Отправка сообщений в очередь для пользователей
    /// </summary>
    /// <param name="classMessage">Класс - сообщение</param>
    public Task SendMessageUserAsync(T classMessage);

    /// <summary>
    /// Отправка сообщений в очередь для администраторов
    /// </summary>
    /// <param name="classMessage">Класс - сообщение</param>
    public Task SendMessageAdminAsync(T classMessage);
}