namespace Api.Controllers.Playlist.Dto.Request;

/// <summary>
/// Запрос на обновление плейлиста
/// </summary>
public record UpdatePlaylistRequest
{
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; init; }
    
    /// <summary>
    /// Является ли приватным
    /// </summary>
    public bool IsPrivate { get; init; }
}