namespace Notification.Infrastructure.Settings.Telegram;

public class TelegramSettings
{
    /// <summary>
    /// Токен для подключения
    /// </summary>
    public string Token { get; set; }
    
    /// <summary>
    /// Id чата, которому нужно присылать оповещения
    /// </summary>
    public long IdChat { get; set; }
}