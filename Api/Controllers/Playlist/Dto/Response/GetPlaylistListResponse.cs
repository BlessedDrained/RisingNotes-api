namespace Api.Controllers.Playlist.Dto.Response;

/// <summary>
/// Ответ на получение списка плейлистов
/// </summary>
public record GetPlaylistListResponse
{
    /// <summary>
    /// Список плейлистов
    /// </summary>
    public List<GetPlaylistInfoResponse> PlaylistList { get; init; } = new();
}