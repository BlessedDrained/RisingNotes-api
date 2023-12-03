namespace Api.Controllers.Song.Dto.Response;

/// <summary>
/// Модель ответа на получение информации о треке
/// </summary>
public record GetAuthorSongInfoResponse
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
    
    /// <summary>
    /// Количество прослушиваний
    /// </summary>
    public int AuditionCount { get; init; }

    /// <summary>
    /// Список жанров
    /// </summary>
    public List<string> GenreList { get; init; } = new();

    /// <summary>
    /// Список языков
    /// </summary>
    public List<string> LanguageList { get; init; } = new();

    /// <summary>
    /// Список настроений
    /// </summary>
    public List<string> VibeList { get; init; } = new();
}