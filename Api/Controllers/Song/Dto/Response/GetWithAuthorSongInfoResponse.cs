namespace Api.Controllers.Song.Dto.Response;

/// <summary>
/// Модель ответа на получение информации о песне с информацией об авторе
/// </summary>
public record GetWithAuthorSongInfoResponse
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Название трека
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Продолжительность трека в секундах
    /// </summary>
    public int DurationMs { get; init; }
    
    /// <summary>
    /// Идентификатор автора
    /// </summary>
    public Guid AuthorId { get; init; }
    
    /// <summary>
    /// Имя автора
    /// </summary>
    public string AuthorName { get; init; }
}