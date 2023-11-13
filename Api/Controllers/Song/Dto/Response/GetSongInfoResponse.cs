namespace Api.Controllers.Song.Dto.Response;

/// <summary>
/// Модель ответа на получение информации о треке
/// </summary>
public record GetSongInfoResponse
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; init; }
    
    /// <summary>
    /// Идентификатор автора
    /// </summary>
    public Guid AuthorId { get; init; }

    /// <summary>
    /// Название трека
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Продолжительность трека в секундах
    /// </summary>
    public int DurationMs { get; init; }
}