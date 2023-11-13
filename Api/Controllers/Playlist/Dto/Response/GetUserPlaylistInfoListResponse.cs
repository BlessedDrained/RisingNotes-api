namespace Api.Controllers.Playlist.Dto.Response;

/// <summary>
/// Ответ на получение списка плейлистов пользователя
/// </summary>
public record GetUserPlaylistInfoListResponse
{
    /// <summary>
    /// Список информации о плейлистах
    /// </summary>
    public List<GetPlaylistInfoResponse> PlaylistInfoList { get; init; } = new();
}