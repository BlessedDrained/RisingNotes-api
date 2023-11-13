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
    /// Соавторы
    /// </summary>
    public List<AddFeaturedAuthorRequest> FeatAuthorList { get; init; } = new();

    // /// <summary>
    // /// Список людей, с кем был фит
    // /// </summary>
    // public List<string> FeatList { get; init; }

    // /// <summary>
    // /// Список, на кого похож
    // /// </summary>
    // // [CorrectSimilarAuthor]
    // // [Required]
    // public List<string> SimilarAuthorList { get; init; } = new();

    // /// <summary>
    // /// Список настроений
    // /// </summary>
    // // [CorrectVibe]
    // // [Required]
    // public List<string> VibeList { get; init; } = new();

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