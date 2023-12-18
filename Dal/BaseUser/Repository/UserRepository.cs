using Dal.Context;
using Dal.Song;
using MainLib.Dal.Exception;
using MainLib.Dal.Repository.Base;
using Microsoft.EntityFrameworkCore;
using RisingNotesLib.Models;

namespace Dal.BaseUser.Repository;

/// <inheritdoc cref="IUserRepository"/>
public class UserRepository : Repository<UserDal, Guid>, IUserRepository
{
    public UserRepository(ApplicationContext context) : base(context)
    {
    }

    /// <inheritdoc />
    public async Task<Guid> GetIdentityUserGuid(Guid userId)
    {
        var identityUserId = await Set
            .Where(x => x.Id == userId)
            .Select(x => x.IdentityUserId)
            .SingleOrDefaultAsync();

        if (identityUserId == null)
        {
            throw new EntityNotFoundException<UserDal>(userId);
        }

        return Guid.Parse(identityUserId);
    }

    /// <inheritdoc />
    public async Task<Guid> GetUserIdByIdentityUserGuid(Guid identityUserId)
    {
        var identityUserIdStr = identityUserId.ToString();
        var userId = await Set
            .Where(x => x.IdentityUserId == identityUserIdStr)
            .Select(x => x.Id)
            .SingleOrDefaultAsync();

        if (userId == Guid.Empty)
        {
            throw new EntityNotFoundException<AppIdentityUser>(identityUserId);
        }

        return userId;
    }

    /// <inheritdoc />
    public async Task<List<SongDal>> GetFavoriteSongInfoListAsync(Guid userId)
    {
        var favoriteSongInfoList = await Set
            .Where(x => x.Id == userId)
            .Include(x => x.FavoriteSongList)
            .ThenInclude(x => x.Author)
            .Select(x => x.FavoriteSongList)
            .SingleOrDefaultAsync();

        if (favoriteSongInfoList == null)
        {
            throw new EntityNotFoundException<UserDal>(userId);
        }

        return favoriteSongInfoList;
    }

    /// <inheritdoc />
    public async Task<UserDal> GetWithFavoriteSongListAsync(Guid userId)
    {
        var user = await Set
            .Where(x => x.Id == userId)
            .Include(x => x.FavoriteSongList)
            .SingleOrDefaultAsync();

        if (user == null)
        {
            throw new EntityNotFoundException<UserDal>(userId);
        }

        return user;
    }

    /// <inheritdoc />
    public async Task<List<Guid>> GetSubscriptionListAsync(Guid userId)
    {
        var subList = await Set
            .Where(x => x.Id == userId)
            .Include(x => x.SubscriptionList)
            .SelectMany(x => x.SubscriptionList)
            .Select(x => x.Id)
            .ToListAsync();

        return subList;
    }

    /// <inheritdoc />
    public async Task<UserDal> GetWithSubscriptionListAsync(Guid userId)
    {
        var user = await Set
            .Where(x => x.Id == userId)
            .Include(x => x.SubscriptionList)
            .SingleOrDefaultAsync();

        if (user == null)
        {
            throw new EntityNotFoundException<UserDal>(userId);
        }

        return user;
    }

    /// <inheritdoc />
    public async Task<UserDal> GetWithExcludedListAsync(Guid userId)
    {
        var user = await Set
            .Where(x => x.Id == userId)
            .Include(x => x.ExcludedSongList)
            .SingleOrDefaultAsync();

        if (user == null)
        {
            throw new EntityNotFoundException<UserDal>(userId);
        }

        return user;
    }

    /// <inheritdoc />
    public async Task<List<SongDal>> GetExcludedTrackListAsync(Guid userId)
    {
        var excludedTrackList = await Set
            .Where(x => x.Id == userId)
            .Include(x => x.ExcludedSongList)
            .ThenInclude(x => x.Author)
            .Select(x => x.ExcludedSongList)
            .SingleOrDefaultAsync();

        if (excludedTrackList == null)
        {
            throw new EntityNotFoundException<UserDal>(userId);
        }

        return excludedTrackList;
    }
}