using Contracts.Interface;
using Contracts.Models.Base;

namespace Contracts.Models.Booking;

public class MessageBooking: BaseMessage, IMessage
{
    #region Свойства

    /// <summary>
    /// Id сообщения
    /// </summary>
    public Guid Id { get; private  set; }
    
    /// <summary>
    /// Id чата для отправки сообщения
    /// </summary>
    public long IdChat { get;  set; }
    
    /// <summary>
    /// Текст сообщения
    /// </summary>
    public string Text { get;  set; }
    
    /// <summary>
    /// Дополнительное описание
    /// </summary>
    public string Description { get;  set; }

    #endregion

    #region Конструктор

    public MessageBooking()
    {
        Id = Guid.NewGuid();
        IdChat = 465309919;
        Text = "Комната забронирована";
        Description = "Дополнительное описание.";
    }

    public MessageBooking(long idChat, string text, string description)
    {
        Id = Guid.NewGuid();
        IdChat = idChat;
        Text = text;
        Description = description;
    }

    #endregion
}