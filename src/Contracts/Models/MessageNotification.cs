using Contracts.Interface;

namespace Contracts.Models;

public class MessageNotification : IMessage
{
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

    public MessageNotification(long idChat, string text, string description)
    {
        Id = Guid.NewGuid();
        IdChat = idChat;
        Text = text;
        Description = description;
    }
}