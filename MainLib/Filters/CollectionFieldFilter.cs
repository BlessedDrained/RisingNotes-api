namespace MainLib.Filters;

/// <summary>
/// Фильтр для поля-коллекции
/// </summary>
public record CollectionFieldFilter<T>
{
    /// <summary>
    /// Список значений
    /// </summary>
    public List<T> ValueList { get; init; } = new();
    
    /// <summary>
    /// Предикат "ИЛИ"
    /// </summary>
    public bool OrPredicate { get; init; }
}