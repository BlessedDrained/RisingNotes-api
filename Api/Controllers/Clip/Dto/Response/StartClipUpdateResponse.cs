namespace Api.Controllers.Clip.Dto.Response;

/// <summary>
/// Запрос на начало обновления файла с клипом
/// </summary>
public record StartClipUpdateResponse
{
    /// <summary>
    /// Идентификатор операции загрузки
    /// </summary>
    public string UploadId { get; init; }
}