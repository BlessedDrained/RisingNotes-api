using Dal.MusicClip;
using Dal.MusicClipComment;

namespace Logic.MusicClipComment;

/// <inheritdoc />
public class MusicClipCommentManager : IMusicClipCommentManager
{
    /// <inheritdoc />
    public Task<Guid> CreateAsync(MusicClipCommentDal clip)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<MusicClipCommentDal> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task UpdateAsync(MusicClipCommentDal clip)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}