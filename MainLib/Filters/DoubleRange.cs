namespace MainLib.Filters;

/// <summary>
/// Range для double
/// </summary>
public record DoubleRange()
{
    /// <summary>
    /// Начальное значение
    /// </summary>
    public double Start { get; init; }
    
    /// <summary>
    /// Конечное значение
    /// </summary>
    public double End { get; init; }
};