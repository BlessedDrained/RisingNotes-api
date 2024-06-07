using Dal.Context;
using IdentityServer4.Stores;
using MainLib.Dal.Exception;
using MainLib.Dal.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Dal.PersistedGrant.Repository;

public class PersistedGrantRepository : Repository<PersistedGrantDal, Guid>, IPersistedGrantRepository
{
    public PersistedGrantRepository(ApplicationContext context) : base(context)
    {
    }

    /// <inheritdoc />
    public async Task<PersistedGrantDal> GetAsync(string key)
    {
        var dal = await Set.FirstOrDefaultAsync(x => x.Key == key);

        if (dal == null)
        {
            throw new EntityNotFoundException<PersistedGrantDal>(key);
        }

        return dal;
    }

    public async Task<IEnumerable<PersistedGrantDal>> GetListAsync(PersistedGrantFilter filter)
    {
        var query = Filter(filter);

        var itemList = await query.ToListAsync();
        return itemList;
    }

    private IQueryable<PersistedGrantDal> Filter(PersistedGrantFilter filter)
    {
        var query = Set.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.ClientId))
        {
            query = query.Where(x => x.ClientId == filter.ClientId);
        }
        if (!string.IsNullOrWhiteSpace(filter.SessionId))
        {
            query = query.Where(x => x.SessionId == filter.SessionId);
        }
        if (!string.IsNullOrWhiteSpace(filter.SubjectId))
        {
            query = query.Where(x => x.SubjectId == filter.SubjectId);
        }
        if (!string.IsNullOrWhiteSpace(filter.Type))
        {
            query = query.Where(x => x.Type == filter.Type);
        }

        return query;
    }
    
    /// <inheritdoc />
    public async Task DeleteAsync(string key)
    {
        var entity = await GetAsync(key);
        Set.Remove(entity);
        await Context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task RemoveAllAsync(PersistedGrantFilter filter)
    {
        var query = Filter(filter);

        await foreach (var t in query.AsAsyncEnumerable())
        {
            Set.Remove(t);
        }

        await Context.SaveChangesAsync();
    }
}