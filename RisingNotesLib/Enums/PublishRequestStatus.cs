namespace RisingNotesLib.Enums;

/// <summary>
/// Статус заявки на публикацию песни
/// </summary>
public enum PublishRequestStatus
{
    /// <summary>
    /// Требует загрузки файла
    /// </summary>
    NeedsSongFile = 0,
    
    /// <summary>
    /// На ревью
    /// </summary>
    Review = 1,
    
    /// <summary>
    /// Требуются правки
    /// </summary>
    RequiresCorrections = 2,
    
    /// <summary>
    /// Принята
    /// </summary>
    Approved = 3,
    
    /// <summary>
    /// Отказана
    /// </summary>
    Rejected = 4,
    
    /// <summary>
    /// Отозвана
    /// </summary>
    Revoked = 5
}