using Dal.Context;
using LinqKit;
using MainLib.Dal.Exception;
using MainLib.Dal.Repository.Base;
using Microsoft.EntityFrameworkCore;
using RisingNotesLib.Models;

namespace Dal.Song.Repository;

/// <inheritdoc cref="ISongRepository" />
public class SongRepository : Repository<SongDal, Guid>, ISongRepository
{
    public SongRepository(ApplicationContext context) : base(context)
    {
    }

    /// <inheritdoc />
    public async Task<SongDal> GetWithAuthorAsync(Guid songId)
    {
        var song = await Set
            .Include(x => x.Author)
            .SingleOrDefaultAsync(x => x.Id == songId);

        if (song == null)
        {
            throw new EntityNotFoundException<SongDal>(songId);
        }

        return song;
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

    /// <inheritdoc />
    public Task<List<SongDal>> GetListAsync(GetSongListFilterModel filter)
    {
        var songList = Set.AsQueryable();
        if (filter == null)
        {
            return songList.ToListAsync();
        }

        if (filter.NamePart != null)
        {
            var kek = filter.NamePart.ToLower();
            songList = songList.Where(x => x.Name.ToLower().Contains(kek));
        }

        if (filter.Gender.HasValue)
        {
            songList = songList.Where(x => x.VocalGenderList.Contains(filter.Gender.Value));
        }

        if (filter.TrackDurationRange != null)
        {
            var range = filter.TrackDurationRange;
            songList = songList.Where(x => x.DurationMsec > range.Start && x.DurationMsec < range.End);
        }

        if (filter.Instrumental.HasValue)
        {
            songList = songList.Where(x => x.Instrumental == filter.Instrumental);
        }

        if (filter.GenreList is {ValueList.Count: > 0})
        {
            var builder = PredicateBuilder.New<SongDal>();
            var genreList = filter.GenreList.ValueList;
            for (var i = 0; i < genreList.Count; i++)
            {
                var genre = genreList[i];
                builder = filter.GenreList.OrPredicate
                    ? builder.Or(x => x.GenreList.Contains(genre))
                    : builder.And(x => x.GenreList.Contains(genre));
            }

            songList = songList.Where(builder);
        }

        if (filter.LanguageList is {ValueList.Count: > 0})
        {
            var builder = PredicateBuilder.New<SongDal>();
            var langList = filter.LanguageList.ValueList;
            for (var i = 0; i < langList.Count; i++)
            {
                var value = langList[i];
                builder = filter.LanguageList.OrPredicate
                    ? builder.Or(x => x.LanguageList.Contains(value))
                    : builder.And(x => x.LanguageList.Contains(value));
            }

            songList = songList.Where(builder);
        }

        if (filter.VibeList is {ValueList.Count: > 0})
        {
            var builder = PredicateBuilder.New<SongDal>();
            var vibeList = filter.VibeList.ValueList;
            for (var i = 0; i < vibeList.Count; i++)
            {
                var value = vibeList[i];
                builder = filter.VibeList.OrPredicate
                    ? builder.Or(x => x.VibeList.Contains(value))
                    : builder.And(x => x.VibeList.Contains(value));
            }

            songList = songList.Where(builder);
        }

        return songList.ToListAsync();
    }
}