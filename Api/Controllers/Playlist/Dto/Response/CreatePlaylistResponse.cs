namespace Api.Controllers.Playlist.Dto.Response;

/// <summary>
/// Модель ответа на создание плейлиста
/// </summary>
public record CreatePlaylistResponse
{
    /// <summary>
    /// Идентификатор плейлиста
    /// </summary>
    public Guid Id { get; init; }
}