namespace Api.Controllers.ExcludedTrack.Dto;

/// <summary>
/// Ответ на получение информации об исключенном треке
/// </summary>
public record GetExcludedTrackInfoResponse
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

    /// <summary>
    /// Список жанров
    /// </summary>
    public List<string> GenreList { get; init; } = new();
}