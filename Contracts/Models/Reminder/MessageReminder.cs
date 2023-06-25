using Contracts.Interface;
using Contracts.Models.Base;

namespace Contracts.Models.Reminder;

public class MessageReminder: BaseMessage, IMessage
{
    #region Свойства

    /// <summary>
    /// Id сообщения
    /// </summary>
    public Guid Id { get; private set; }
    
    /// <summary>
    /// Id чата для отправки сообщения
    /// </summary>
    public long IdChat { get; set; }
    
    /// <summary>
    /// Текст сообщения
    /// </summary>
    public string Text { get; set; }
    
    /// <summary>
    /// Дополнительное описание
    /// </summary>
    public string Description { get; set; }

    #endregion

    #region Конструктор

    public MessageReminder()
    {
        Id = Guid.NewGuid();
        IdChat = 465309919;
        Text = "Оповещение - напоминание о бронировании";
        Description = "Дополнительное описание";
    }

    public MessageReminder(long idChat, string text, string description)
    {
        Id = Guid.NewGuid();
        IdChat = idChat;
        Text = text;
        Description = description;

    }

    #endregion
}