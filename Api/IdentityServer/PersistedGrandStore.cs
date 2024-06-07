using AutoMapper;
using Dal.PersistedGrant;
using Dal.PersistedGrant.Repository;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace Api.IdentityServer;

/// <summary>
/// <see cref="IPersistedGrandStore"/>, которое хранит данные в БД
/// </summary>
public class PersistedGrandStore : IPersistedGrantStore
{
    private readonly IMapper _mapper;
    private readonly IPersistedGrantRepository _repository;

    public PersistedGrandStore(IMapper mapper, IPersistedGrantRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    /// <inheritdoc />
    public async Task StoreAsync(PersistedGrant grant)
    {
        var dal = _mapper.Map<PersistedGrantDal>(grant);
        await _repository.InsertAsync(dal);
    }

    /// <inheritdoc />
    public async Task<PersistedGrant> GetAsync(string key)
    {
        var dal = await _repository.GetAsync(key);

        var model = _mapper.Map<PersistedGrant>(dal);

        return model;
    }

    public async Task<IEnumerable<PersistedGrant>> GetAllAsync(PersistedGrantFilter filter)
    {
        var grantList = await _repository.GetListAsync(filter);

        var modelList = _mapper.Map<List<PersistedGrant>>(grantList);

        return modelList;
    }

    /// <inheritdoc />
    public async Task RemoveAsync(string key)
    {
        await _repository.DeleteAsync(key);
    }

    /// <inheritdoc />
    public async Task RemoveAllAsync(PersistedGrantFilter filter)
    {
        await _repository.RemoveAllAsync(filter);
    }
}