using Api.Controllers.File.Dto.Request;
using Api.Validation;

namespace Api.Controllers.SongPublish.Dto.Request;

/// <summary>
/// Запрос на ответ на заявку от лица пользователя
/// </summary>
public record ReplyToRequestAsUserRequest
{
    /// <summary>
    /// Название песни
    /// </summary>
    public string SongName { get; init; }
    
    /// <summary>
    /// Текст песни
    /// </summary>
    public string Lyrics { get; init; }
    
    /// <summary>
    /// Файл песни
    /// </summary>
    [MaxFileSize(30720)]
    public UploadFileRequest SongFile { get; init; }
    
    /// <summary>
    /// Файл логотипа
    /// </summary>
    [MaxFileSize(2048)]
    public UploadFileRequest LogoFile { get; init; }
}