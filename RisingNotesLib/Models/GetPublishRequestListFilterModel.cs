namespace RisingNotesLib.Models;

public record GetPublishRequestListFilterModel
{
    /// <summary>
    /// Сортировать по статусу
    /// </summary>
    public bool? OrderByStatusDescending { get; init; }

    /// <summary>
    /// Сортировать по имени автора
    /// </summary>
    /// <remarks>null - не сортировать, false - по возрастанию</remarks>
    public bool? OrderByAuthorNameDescending { get; init; }
    
    /// <summary>
    /// Для пагинации
    /// </summary>
    public int? Offset { get; init; }
    
    /// <summary>
    /// для пагинации
    /// </summary>
    public int? Count { get; init; }
}