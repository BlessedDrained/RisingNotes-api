using Destructurama.Attributed;

namespace Api.Controllers.Song.Dto.Request;

/// <summary>
/// Запрос на загрузку логотипа для песни
/// </summary>
public record UploadSongLogoRequest
{
    /// <summary>
    /// Идентификатор песни
    /// </summary>
    public Guid SongId { get; init; }
    
    /// <summary>
    /// Файл с логотипом
    /// </summary>
    [LogMasked]
    public IFormFile LogoFile { get; init; }
}