using MainLib.Dal.Repository.Base;

namespace Dal.SongComment.Repository;

/// <summary>
/// Репозиторий для комментариев к песням
/// </summary>
public interface ISongCommentRepository : IRepository<SongCommentDal, Guid>
{
    /// <summary>
    /// Получить список комментариев к песне
    /// </summary>
    public Task<List<SongCommentDal>> GetSongCommentListAsync(Guid songId);
}