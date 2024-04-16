using Dal.ShortVideo;
using Dal.ShortVideo.Repository;

namespace Logic.ShortVideo;

/// <inheritdoc />
public class ShortVideoManager : IShortVideoManager
{
    private readonly IShortVideoRepository _repository;

    public ShortVideoManager(IShortVideoRepository repository)
    {
        _repository = repository;

    }
    
    /// <inheritdoc />
    public Task<Guid> CreateAsync(ShortVideoDal clip)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<ShortVideoDal> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task UpdateAsync(ShortVideoDal clip)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}