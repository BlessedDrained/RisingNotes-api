using Dal.ShortVideoComment;

namespace Logic.ShortVideoComment;

/// <inheritdoc />
public class ShortVideoCommentManager : IShortVideoCommentManager
{
    /// <inheritdoc />
    public Task<Guid> CreateAsync(ShortVideoCommentDal clip)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<ShortVideoCommentDal> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task UpdateAsync(ShortVideoCommentDal clip)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}