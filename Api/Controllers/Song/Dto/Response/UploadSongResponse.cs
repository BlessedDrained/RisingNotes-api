namespace Api.Controllers.Song.Dto.Response;

/// <summary>
/// Модель ответа на создание трека
/// </summary>
public record UploadSongResponse
{
    /// <summary>
    /// Идентификатор созданного черновика
    /// </summary>
    public Guid Id { get; init; }
}