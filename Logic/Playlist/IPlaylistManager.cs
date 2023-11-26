using Dal.File;
using Dal.Playlist;
using Dal.Song;

namespace Logic.Playlist;

/// <summary>
/// Менеджер для плейлистов
/// </summary>
public interface IPlaylistManager
{
    /// <summary>
    /// Создать плейлист
    /// </summary>
    Task<Guid> CreateAsync(PlaylistDal playlist);

    /// <summary>
    /// Получить информацию о плейлисте
    /// </summary>
    /// <remarks>не отдает список треков</remarks>
    Task<PlaylistDal> GetInfoAsync(Guid playlistId);

    /// <summary>
    /// Получить список информации о плейлистах
    /// </summary>
    /// <param name="userId"></param>
    /// <remarks>Не включает в себя информацию о треках!</remarks>
    Task<List<PlaylistDal>> GetUserPlaylistInfoList(Guid userId);

    /// <summary>
    /// Получить список песен в плейлисте
    /// </summary>
    Task<List<SongDal>> GetSongListAsync(Guid playlistId);

    /// <summary>
    /// Добавить трек в плейлист
    /// </summary>
    Task AddTrackAsync(Guid playlistId, Guid songId);

    /// <summary>
    /// Получить файл с логотипом
    /// </summary>
    Task<FileDal> GetLogoAsync(Guid playlistId);
    
    /// <summary>
    /// Обновить логотип
    /// </summary>
    Task UpdateLogoAsync(Guid userId, Guid playlistId, FileDal file);
}