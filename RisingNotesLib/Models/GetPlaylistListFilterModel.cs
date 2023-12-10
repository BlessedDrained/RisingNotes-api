namespace RisingNotesLib.Models;

/// <summary>
/// Модель фильтра для поиска плейлистов
/// </summary>
public record GetPlaylistListFilterModel
{
    /// <summary>
    /// Часть имени
    /// </summary>
    public string NamePart { get; init; }
}