namespace Contracts.Models.Base;

/// <summary>
/// Базовый класс для моделей сообщений, используется для маппинга свойств в десериализации
/// </summary>
public class BaseMessage
{
    #region Свойства

    /// <summary>
    /// Id сообщения
    /// </summary>
    public Guid Id { get;  set; }

    /// <summary>
    /// Id чата для отправки сообщения
    /// </summary>
    public long IdChat { get ;  set; }

    /// <summary>
    /// Текст сообщения
    /// </summary>
    public string Text { get;  set; }
    
    /// <summary>
    /// Дополнительное описание
    /// </summary>
    public string Description { get;  set; }

    #endregion
}