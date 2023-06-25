using Contracts.Interface;
using Contracts.Models.Base;

namespace Contracts.Models.Unbooking;

public class MessageUnbooking: BaseMessage, IMessage
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

    public MessageUnbooking()
    {
        Id = Guid.NewGuid();
        IdChat = -1001961900437;
        Text = "Комната разбронирована";
        Description = "Дополнительное описание.";
    }

    public MessageUnbooking(long idChat, string text, string description)
    {
        Id = Guid.NewGuid();
        IdChat = idChat;
        Text = text;
        Description = description;
    }

    #endregion
}