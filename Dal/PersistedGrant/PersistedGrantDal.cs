using MainLib.Dal.Model.Base;

namespace Dal.PersistedGrant;

public record PersistedGrantDal : DalModel<Guid>
{
    /// <summary>
    /// Ключ
    /// </summary>
    public string Key { get; set; }
    
    /// <summary>
    /// Тип
    /// </summary>
    public string Type { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public string SubjectId { get; set; }
    
    /// <summary>
    /// Идентификатор сессии
    /// </summary>
    public string SessionId { get; set; }
    
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public string ClientId { get; set; }
    
    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreationTime { get; set; }
    
    /// <summary>
    /// Дата истечения
    /// </summary>
    public DateTime? Expiration { get; set; }
    
    /// <summary>
    /// Время, когда сущность была использована
    /// </summary>
    public DateTime? ConsumedTime { get; set; }
    
    /// <summary>
    /// Данные
    /// </summary>
    public string Data { get; set; }
}