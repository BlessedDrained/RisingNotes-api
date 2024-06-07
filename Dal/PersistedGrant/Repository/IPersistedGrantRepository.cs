using IdentityServer4.Stores;
using MainLib.Dal.Repository.Base;

namespace Dal.PersistedGrant.Repository;


/// <summary>
/// <see cref="PersistedGrantDal"/>
/// </summary>
public interface IPersistedGrantRepository : IRepository<PersistedGrantDal, Guid>
{
    Task<PersistedGrantDal> GetAsync(string key);

    /// <summary>
    /// Удалить по ключу
    /// </summary>
    Task DeleteAsync(string key);

    /// <summary>
    /// Удалить все
    /// </summary>
    Task RemoveAllAsync(PersistedGrantFilter filter);

    /// <summary>
    /// Получить список по фильтру
    /// </summary>
    Task<IEnumerable<PersistedGrantDal>> GetListAsync(PersistedGrantFilter filter);
}