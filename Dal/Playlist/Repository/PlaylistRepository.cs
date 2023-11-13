using Dal.Context;
using MainLib.Dal.Exception;
using MainLib.Dal.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Dal.Playlist.Repository;

/// <summary>
/// Репозиторий для плейлистов
/// </summary>
public class PlaylistRepository : Repository<PlaylistDal, Guid>, IPlaylistRepository
{
    public PlaylistRepository(ApplicationContext context) : base(context)
    {
    }

    /// <inheritdoc />
    public async Task<PlaylistDal> GetWithSongListAsync(Guid playlistId)
    {
        var playlist = await Set
            .Where(x => x.Id == playlistId)
            .Include(x => x.SongList)
            .SingleOrDefaultAsync();

        if (playlist == null)
        {
            throw new EntityNotFoundException<PlaylistDal>(playlistId);
        }

        return playlist;
    }
}