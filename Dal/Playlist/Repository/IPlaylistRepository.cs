using MainLib.Dal.Repository.Base;

namespace Dal.Playlist.Repository;

public interface IPlaylistRepository : IRepository<PlaylistDal, Guid>
{
    /// <summary>
    /// Получить со списком песен
    /// </summary>
    /// <param name="playlistId"></param>
    /// <returns></returns>
    Task<PlaylistDal> GetWithSongListAsync(Guid playlistId);
}