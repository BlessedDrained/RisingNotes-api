namespace Api.Controllers.ShortVideo.Dto.Response;

/// <summary>
/// ответ на создание клипа
/// </summary>
public record UploadShortVideoResponse
{
    /// <summary>
    /// Идентификатор клипа
    /// </summary>
    public Guid Id { get; init; }
}