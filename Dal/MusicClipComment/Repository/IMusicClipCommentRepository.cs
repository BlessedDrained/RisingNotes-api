using MainLib.Dal.Repository.Base;

namespace Dal.MusicClipComment.Repository;

/// <summary>
/// Репозиторий для <see cref="MusicClipCommentDal"/>
/// </summary>
public interface IMusicClipCommentRepository : IRepository<MusicClipCommentDal, Guid>
{
    
}