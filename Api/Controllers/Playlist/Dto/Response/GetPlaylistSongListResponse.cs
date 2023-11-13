using Api.Controllers.Song.Dto.Response;

namespace Api.Controllers.Playlist.Dto.Response;

/// <summary>
/// Модель ответа на получение списка песен плейлиста 
/// </summary>
public record GetPlaylistSongListResponse
{
    /// <summary>
    /// Информация о песнях
    /// </summary>
    public List<GetWithAuthorSongInfoResponse> SongList { get; init; } = new();
}