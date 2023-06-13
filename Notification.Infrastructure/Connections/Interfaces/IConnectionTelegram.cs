using Notification.Infrastructure.Settings.Telegram;
using Telegram.Bot;

namespace Notification.Infrastructure.Connections.Interfaces;

public interface IConnectionTelegram
{
    /// <summary>
    /// Подключение к Telegram
    /// </summary>
    public TelegramBotClient BotClient { get; }
    
    /// <summary>
    /// Настройки Telegram
    /// </summary>
    public TelegramSettings Settings { get; }
}