using Microsoft.Extensions.Options;

namespace Notification.Infrastructure.Settings;

public class NotificationInfrastructureOptions: IOptions<NotificationInfrastructureSettings>
{
    public NotificationInfrastructureSettings Value { get; }
}