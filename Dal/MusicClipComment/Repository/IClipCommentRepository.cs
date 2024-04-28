using MainLib.Dal.Repository.Base;

namespace Dal.MusicClipComment.Repository;

/// <summary>
/// Репозиторий для <see cref="ClipCommentDal"/>
/// </summary>
public interface IClipCommentRepository : IRepository<ClipCommentDal, Guid>
{
    /// <summary>
    /// Получить список комментариев к песне
    /// </summary>
    public Task<List<ClipCommentDal>> GetClipCommentListAsync(Guid songId);
}