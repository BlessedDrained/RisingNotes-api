namespace Logic.File;

/// <summary>
/// Метаинформация о клипе
/// </summary>
public record FileMetadata
{
    /// <summary>
    /// Размер клипа в байтах
    /// </summary>
    public long SizeBytes { get; init; }
    
    /// <summary>
    /// Content-Type
    /// </summary>
    public string ContentType { get; init; }
    
    /// <summary>
    /// Количество частей файла
    /// </summary>
    public int? PartCount { get; init; }
}