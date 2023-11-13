namespace RisingNotesLib.Models;

public record GetPublishRequestListFilterModel
{
    /// <summary>
    /// Сортировать по статусу
    /// </summary>
    public bool? OrderByStatus { get; init; }

    /// <summary>
    /// Сортировать по имени автора
    /// </summary>
    public bool? OrderByAuthorName { get; init; }
    
    /// <summary>
    /// Для пагинации
    /// </summary>
    public int? Offset { get; init; }
    
    /// <summary>
    /// для пагинации
    /// </summary>
    public int? Count { get; init; }
}