namespace RisingNotesLib.Enums;

/// <summary>
/// Статус песни
/// </summary>
public enum SongStatus
{
    /// <summary>
    /// Значение по умолчанию
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Ожидает модерации
    /// </summary>
    Moderation = 1,

    /// <summary>
    /// Не прошло модерацию
    /// </summary>
    Rejected = 2,
    
    /// <summary>
    /// Опубликовано
    /// </summary>
    Published = 3
}