namespace Notification.Infrastructure.Settings.Telegram;

/// <summary>
/// Настройки Telegram
/// </summary>
public class TelegramSettings
{
    /// <summary>
    /// Токен для подключения к чату пользователей
    /// </summary>
    public string TokenBotUser { get; set; }
    
    /// <summary>
    /// Токен для подключения к чату администратора
    /// </summary>
    public string TokenBotAdmin { get; set; }
    
    /// <summary>
    /// Id канала с администраторами
    /// </summary>
    public long AdminsChannel { get; set; }
}