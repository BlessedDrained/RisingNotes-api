using MainLib.Dal.Repository.Base;

namespace Dal.Comment.Repository;

/// <summary>
/// Репозиторий для комментариев к песням
/// </summary>
public interface ICommentRepository : IRepository<CommentDal, Guid>
{
    /// <summary>
    /// Получить список комментариев к песне
    /// </summary>
    public Task<List<CommentDal>> GetSongCommentListAsync(Guid songId);
}