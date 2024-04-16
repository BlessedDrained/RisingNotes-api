using MainLib.Dal.Repository.Base;

namespace Dal.ShortVideoComment.Repository;

/// <summary>
/// Репозиторий для <see cref="ShortVideoCommentDal"/>
/// </summary>
public interface IShortVideoCommentRepository : IRepository<ShortVideoCommentDal, Guid>
{
    
}