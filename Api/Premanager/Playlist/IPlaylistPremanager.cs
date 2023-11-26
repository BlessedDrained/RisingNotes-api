using Api.Controllers.File.Dto.Request;
using Api.Controllers.Playlist.Dto.Request;
using Api.Controllers.Playlist.Dto.Response;

namespace Api.Premanager.Playlist;

/// <summary>
/// Premanager для плейлистов
/// </summary>
public interface IPlaylistPremanager
{
    /// <summary>
    /// Создать плейлист
    /// </summary>
    Task<CreatePlaylistResponse> CreateAsync(CreatePlaylistRequest request, Guid userId);

    /// <summary>
    /// Получить информацию о плейлисте
    /// </summary>
    Task<GetPlaylistInfoResponse> GetInfoAsync(Guid playlistId);

    /// <summary>
    /// Получить список информации о плейлистах пользователя
    /// </summary>
    Task<GetUserPlaylistInfoListResponse> GetUserPlaylistInfoListAsync(Guid userId);

    /// <summary>
    /// Получить список песен в плейлисте
    /// </summary>
    Task<GetPlaylistSongListResponse> GetSongListAsync(Guid playlistId);
    
    /// <summary>
    /// Обновить логотип
    /// </summary>
    Task UpdateLogoAsync(Guid userId, Guid playlistId, UploadFileRequest request);
}