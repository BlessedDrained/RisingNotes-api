using Api.Controllers.File.Dto.Request;
using Api.Validation;

namespace Api.Controllers.Song.Dto.Request;

/// <summary>
/// Модель запроса на создание трека
/// </summary>
public record UploadSongRequest
{
    /// <summary>
    /// Название
    /// </summary>
    // [Required]
    public string SongName { get; init; }

    /// <summary>
    /// Список жанров песни
    /// </summary>
    public string[] GenreList { get; init; } = Array.Empty<string>();

    /// <summary>
    /// Список настроений
    /// </summary>
    public string[] VibeList { get; init; } = Array.Empty<string>();

    /// <summary>
    /// Список языков
    /// </summary>
    public string[] LanguageList { get; init; } = Array.Empty<string>();
    
    /// <summary>
    /// Является ли инструментальной
    /// </summary>
    public bool Instrumental { get; init; }

    /// <summary>
    /// Имеет ли текст
    /// </summary>
    // [Required]
    public bool HasLyrics { get; init; }

    // TODO: добавить ограничение на длину
    public string Lyrics { get; init; }

    /// <summary>
    /// Содержит ненормативную лексику и т.д
    /// </summary>
    // [Required]
    public bool Explicit { get; init; }

    /// <summary>
    /// Файл с треком
    /// </summary>
    // [Required]
    [MaxFileSize(30720)]
    public UploadFileRequest SongFile { get; init; }

    /// <summary>
    /// Лого трека
    /// </summary>
    [MaxFileSize(2048)]
    public UploadFileRequest SongLogo { get; init; }
}