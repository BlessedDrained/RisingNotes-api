using Api.Controllers.File.Dto.Request;

namespace Api.Controllers.Clip.Dto.Request;

/// <summary>
/// Запрос на загрузку клипа
/// </summary>
public record UploadClipRequest
{
    /// <summary>
    /// Название
    /// </summary>
    public string Title { get; init; }
    
    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; init; }
    
    /// <summary>
    /// Идентификатор песни
    /// </summary>
    public Guid SongId { get; init; }

    /// <summary>
    /// Файл с превью
    /// </summary>
    public UploadFileRequest PreviewFile { get; init; }

    /// <summary>
    /// Файл с клипом
    /// </summary>
    public UploadFileRequest ClipFile { get; init; }
}