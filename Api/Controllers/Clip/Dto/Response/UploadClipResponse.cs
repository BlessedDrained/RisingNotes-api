namespace Api.Controllers.Clip.Dto.Response;

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