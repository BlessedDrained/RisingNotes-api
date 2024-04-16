namespace Api.Controllers.MusicClip.Dto.Response;

/// <summary>
/// ответ на создание клипа
/// </summary>
public record UploadClipResponse
{
    /// <summary>
    /// Идентификатор клипа
    /// </summary>
    public Guid Id { get; init; }
}