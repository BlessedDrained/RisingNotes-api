namespace Api.Controllers.Song.Dto.Response;

/// <summary>
/// Модель ответа на создание трека
/// </summary>
public record CreateSongResponse
{
    /// <summary>
    /// Идентификатор созданного трека
    /// </summary>
    public Guid Id { get; init; }
}