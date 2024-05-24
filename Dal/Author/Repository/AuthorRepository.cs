using Dal.BaseUser;
using Dal.Context;
using Dal.Song;
using MainLib.Dal.Exception;
using MainLib.Dal.Repository.Base;
using Microsoft.EntityFrameworkCore;
using RisingNotesLib.Models;

namespace Dal.Author.Repository;

/// <inheritdoc cref="IAuthorRepository"/>
public class AuthorRepository : Repository<AuthorDal, Guid>, IAuthorRepository
{
    public AuthorRepository(ApplicationContext context) : base(context)
    {
    }

    /// <inheritdoc />
    public async Task<AuthorDal> GetShortInfoAsync(string authorName)
    {
        var author = await Set
            .Include(x => x.User)
            .SingleOrDefaultAsync(x => x.Name == authorName);
        
        if (author == null)
        {
            throw new EntityNotFoundException<AuthorDal>(authorName);
        }

        return author;
    }

    /// <inheritdoc />
    public async Task<AuthorDal> GetShortInfoAsync(Guid authorId)
    {
        var author = await Set
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == authorId);
        
        if (author == null)
        {
            throw new EntityNotFoundException<AuthorDal>(authorId);
        }

        return author;
    }

    /// <inheritdoc />
    public async Task<AuthorDal> GetByUserIdAsync(Guid userId)
    {
        var author = await Set
            .Include(x => x.User)
            .SingleOrDefaultAsync(x => x.UserId == userId);
        
        if (author == null)
        {
            throw new EntityNotFoundException<AuthorDal>($"""userId = {userId}""");
        }

        return author;
    }

    /// <inheritdoc />
    public async Task<AuthorDal> GetInfoAsync(string authorName)
    {
        var user = await Context
            .Set<UserDal>()
            .Where(x => x.UserName == authorName)
            .FirstOrDefaultAsync();
        
        if (user == null)
        {
            throw new EntityNotFoundException<UserDal>(authorName);
        }
        
        var author = await Set
            .Include(x => x.User)
            .Where(x => x.UserId == user.Id)
            .FirstOrDefaultAsync();
        
        if (author == null)
        {
            throw new EntityNotFoundException<AuthorDal>(authorName);
        }

        return author;
    }

    /// <inheritdoc />
    public Task<List<AuthorDal>> GetListAsync(GetAuthorListFilterModel filter)
    {
        var authorList = Set.Include(x => x.User)
            .AsQueryable();

        if (filter == null)
        {
            return authorList
                .ToListAsync();
        }

        if (filter.NameWildcard != null)
        {
            var kek = filter.NameWildcard.ToLower();
            authorList = authorList.Where(x => x.User.UserName.ToLower().Contains(kek));
            // .Where(x => 1 - EF.Functions.FuzzyStringMatchLevenshtein(filter.NameWildcard, x.AuthorPseudoname) / Math.Max(x.AuthorPseudoname.Length, filter.NameWildcard.Length) > 0.8);
        }

        var result = authorList.ToListAsync();
        return result;
    }

    /// <inheritdoc />
    public async Task<int> GetSubcriberCountAsync(Guid authorId)
    {
        var author = await Set
            .Include(x => x.User)
            .SingleOrDefaultAsync(x => x.Id == authorId);
        
        if (author == null)
        {
            throw new EntityNotFoundException<AuthorDal>(authorId);
        }

        var count = await Set
            .Where(x => x.Id == authorId)
            .Select(x => x.SubscribedUserList.Count)
            .SingleAsync();

        return count;
    }

    /// <inheritdoc />
    public async Task<int> GetTotalAuditionCountAsync(Guid authorId)
    {
        var songSet = Context.Set<SongDal>();

        var auditionCount = await songSet
            .Where(x => x.AuthorId == authorId)
            .Select(x => x.AuditionCount)
            .SumAsync();

        return auditionCount;
    }

    public override async Task<AuthorDal> GetAsync(Guid id)
    {
        var author = await Set.Include(x => x.User)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
        
        if (author == null)
        {
            throw new EntityNotFoundException<AuthorDal>(id);
        }

        return author;
    }
}