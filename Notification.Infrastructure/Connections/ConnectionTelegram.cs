using Microsoft.Extensions.Options;
using Notification.Infrastructure.Connections.Interfaces;
using Notification.Infrastructure.Settings;
using Notification.Infrastructure.Settings.Telegram;
using Telegram.Bot;

namespace Notification.Infrastructure.Connections;

public class ConnectionTelegram: IConnectionTelegram
{
    #region Свойства

    public TelegramSettings Settings { get; }
    
    public TelegramBotClient BotClient { get; }

    #endregion

    #region Конструктор

    public ConnectionTelegram(IOptions<NotificationInfrastructureSettings> settings)
    {
        Settings = settings.Value.TelegramSettings;
        BotClient = new TelegramBotClient(Settings.Token);
    }

    #endregion
}