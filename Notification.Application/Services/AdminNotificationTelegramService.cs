using Notification.Application.Interfaces;
using Notification.Infrastructure.Interfaces.Connections;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Notification.Application.Services;

public class AdminNotificationTelegramService: IAdminNotification
{
    #region Поле

    /// <summary>
    /// Подключение к Telegram
    /// </summary>
    private readonly IConnectionTelegram _connection;

    #endregion
    
    #region Конструктор
     
    public AdminNotificationTelegramService(IConnectionTelegram connection)
    {
        _connection = connection;
        _connection.BotClientAdmin.StartReceiving(UpdateBotAsync, ExceptionBotAsync);
    }
     
    #endregion
    
    #region Методы
     
    /// <summary>
    /// Отправить сообщение с помощью бота
    /// </summary>
    /// <param name="message">Сообщение</param>
    public async Task SendMessage(string message)
    {
        if (message != null)
        {
            await _connection.BotClientAdmin.SendTextMessageAsync(_connection.Settings.AdminsChannel, message);
        }
    }

    /// <summary>
    /// В автоматическом режиме получает сообщения, обрабатывает их
    /// </summary>
    private async Task UpdateBotAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        //TODO обработка сообщений и ответ на них
    }
     
    /// <summary>
    /// В автоматическом режиме обрабатывает ошибки
    /// </summary>
    private async Task ExceptionBotAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        //TODO обработка ошибок
    }
     
    #endregion
}