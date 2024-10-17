using System.Linq.Expressions;
using MainLib.Dal.Exception;
using MainLib.Dal.Model.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace MainLib.Dal.Repository.Base;

/// <summary>
/// Базовый класс репозитория
/// </summary>
/// <typeparam name="TEntity">Тип модели</typeparam>
/// <typeparam name="TKey">Тип первичного ключа</typeparam>
public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : class, IDalModel<TKey>
{
    protected readonly DbContext Context;
    protected readonly DbSet<TEntity> Set;

    /// <summary>
    /// Конструктор
    /// </summary>
    protected Repository(DbContext context)
    {
        Context = context;
        Set = Context.Set<TEntity>();
    }

    /// <inheritdoc />
    public virtual async Task<TKey> InsertAsync(TEntity entity)
    {
        Set.Add(entity);
        await Context.SaveChangesAsync();
        return entity.Id;
    }

    /// <inheritdoc />
    public virtual async Task<TEntity> GetAsync(TKey id)
    {
        var foundEntity = await Set.FindAsync(id);
        if (foundEntity == null)
        {
            throw new EntityNotFoundException<TEntity>(id);
        }

        return foundEntity;
    }

    /// <inheritdoc />
    public virtual async Task<IEnumerable<TEntity>> GetByIdListAsync(IEnumerable<TKey> idList)
    {
        var foundEntityList = Set.Where(x => idList.Contains(x.Id));
        return foundEntityList;
    }

    /// <inheritdoc />
    public virtual Task UpdateAsync(TEntity entity)
    {
        Set.Update(entity);
        return Context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public virtual async Task DeleteAsync(TKey id)
    {
        var toRemoveEntity = await GetAsync(id);
        Set.Remove(toRemoveEntity);
        await Context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null)
    {
        if (predicate == null)
        {
            return await Set.ToListAsync();
        }
        
        var result = await Set
            .Where(predicate)
            .ToListAsync();

        return result;
    }

    // /// <inheritdoc />
    // public virtual async Task<IEnumerable<TEntity>> GetListAsync(
    //     Expression<Func<TEntity, bool>>? filter = null,
    //     Expression<Func<TEntity, object>>? orderBy = null,
    //     bool descending = false)
    // {
    //     var result = Set.AsQueryable();
    //     if (filter != null)
    //     {
    //         result = Set.Where(filter);
    //     }
    //
    //     if (orderBy != null)
    //     {
    //         result = descending
    //             ? result.OrderByDescending(orderBy)
    //             : result.OrderBy(orderBy);
    //     }
    //
    //     return result;
    // }

    /// <inheritdoc/>
    public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null)
    {
        var result = Set.AsQueryable();
        if (filter != null)
        {
            result = Set.Where(filter);
        }

        return result.CountAsync();
    }

    /// <inheritdoc/>
    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>>? filter)
    {
        var result = Set.AsQueryable();

        if (filter == null)
        {
            return await result.AnyAsync();
        }

        return await result.AnyAsync(filter);

    }

    /// <inheritdoc />
    public virtual async Task<TEntity> FirstByFieldAsync(Expression<Func<TEntity, bool>> filter)
    {
        ArgumentNullException.ThrowIfNull(filter);

        var result = await Set.FirstOrDefaultAsync(filter);

        if (result == default)
        {
            throw new EntityNotFoundException<TEntity>();
        }

        return result;
    }

    /// <inheritdoc />
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        var transacton =  await Context.Database.BeginTransactionAsync();

        return transacton;
    }

    public async Task<IDbContextTransaction> BeginTransactionOrExistingAsync()
    {
        return default;
        // return Context.Database.CurrentTransaction ?? await Context.Database.BeginTransactionAsync();
    }
}