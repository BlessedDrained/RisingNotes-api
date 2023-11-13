using Dal.Context;
using MainLib.Dal.Exception;
using MainLib.Dal.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Dal.Song.Repository;

/// <inheritdoc cref="ISongRepository" />
public class SongRepository : Repository<SongDal, Guid>, ISongRepository
{
    public SongRepository(ApplicationContext context) : base(context)
    {
    }

    /// <inheritdoc />
    public async Task<SongDal> GetFullAsync(Guid songId)
    {
        var song = await Set
            .Where(x => x.Id == songId)
            .Include(x => x.SongFile)
            .SingleOrDefaultAsync();

        if (song == null)
        {
            throw new EntityNotFoundException<SongDal>(songId);
        }
        
        return song;
    }
}