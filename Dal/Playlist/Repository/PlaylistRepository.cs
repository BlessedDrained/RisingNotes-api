using Dal.Context;
using MainLib.Dal.Exception;
using MainLib.Dal.Repository.Base;
using Microsoft.EntityFrameworkCore;
using RisingNotesLib.Models;

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

    /// <inheritdoc />
    public Task<List<PlaylistDal>> GetListAsync(Guid? userId, GetPlaylistListFilterModel filterModel)
    {
        var playlistList = Set.AsQueryable();

        if (filterModel.NamePart != null)
        {
            var kek = filterModel.NamePart.ToLower();
            playlistList = playlistList.Where(x => x.Name.ToLower().Contains(kek));
        }

        playlistList = playlistList.Where(x => !x.IsPrivate || x.IsPrivate && x.CreatorId == userId);

        return playlistList.ToListAsync();
    }
}