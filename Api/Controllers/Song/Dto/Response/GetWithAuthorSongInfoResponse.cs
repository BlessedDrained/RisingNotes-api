using RisingNotesLib.Enums;

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
    /// Текст трека
    /// </summary>
    public string Lyrics { get; init; }

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

    /// <summary>
    /// 
    /// </summary>
    public List<Gender> VocalGenderList { get; init; } = new();
}