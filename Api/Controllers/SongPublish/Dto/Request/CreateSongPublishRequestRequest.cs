using Api.Controllers.File.Dto.Request;
using Api.Validation;
using Dal.File;

namespace Api.Controllers.SongPublish.Dto.Request;

/// <summary>
/// Запрос на создание заявки на публикацию песни
/// </summary>
public record CreateSongPublishRequestRequest
{
    /// <summary>
    /// Название трека
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Текст песни
    /// </summary>
    public string Lyrics { get; set; }

    /// <summary>
    /// Нав свойство
    /// </summary>
    [MaxFileSize(30720)]
    public UploadFileRequest SongFile { get; set; }

    /// <summary>
    /// Нав свойство
    /// </summary>
    [MaxFileSize(2048)]
    public UploadFileRequest LogoFile { get; set; }
}