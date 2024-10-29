namespace Api.Controllers.SongPublish.Dto.Response;

/// <summary>
/// Запрос на начало обновления файла с клипом
/// </summary>
public record StartSongFileUpdateResponse
{
    /// <summary>
    /// Идентификатор операции загрузки
    /// </summary>
    public string UploadId { get; init; }
}