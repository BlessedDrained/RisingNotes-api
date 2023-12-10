using MainLib.Dal.Repository.Base;
using RisingNotesLib.Models;

namespace Dal.Playlist.Repository;

public interface IPlaylistRepository : IRepository<PlaylistDal, Guid>
{
    /// <summary>
    /// Получить со списком песен
    /// </summary>
    Task<PlaylistDal> GetWithSongListAsync(Guid playlistId);

    /// <summary>
    /// Получить список плейлистов по фильтрам
    /// </summary>
    Task<List<PlaylistDal>> GetListAsync(Guid? userId, GetPlaylistListFilterModel filterModel);
}