using Notification.Infrastructure.Settings.Telegram;
using Telegram.Bot;

namespace Notification.Infrastructure.Interfaces.Connections;

/// <summary>
/// Подключение к Telegram
/// </summary>
public interface IConnectionTelegram
{
    /// <summary>
    /// Bot Telegram, для администраторов 
    /// </summary>
    public TelegramBotClient BotClientAdmin { get; }
    
    /// <summary>
    /// Bot Telegram, для пользователей
    /// </summary>
    public TelegramBotClient BotClientUser { get; }
    
    /// <summary>
    /// Настройки Telegram
    /// </summary>
    public TelegramSettings Settings { get; }
}