namespace Application.RabbitMQ.Interfaces;

public interface INotificationRabbitMQ
{
    /// <summary>
    /// Оповещение о бронировании комнаты
    /// </summary>
    public void Notification();
    
    /// <summary>
    /// Оповещение, напоминание о забронированной комнаты
    /// </summary>
    public void BookingReminderNotification();
}