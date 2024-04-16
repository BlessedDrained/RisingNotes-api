using MainLib.Dal.Repository.Base;

namespace Dal.MusicClip.Repository;

/// <summary>
/// Репозиторий для <see cref="MusicClipDal"/>
/// </summary>
public interface IMusicClipRepository : IRepository<MusicClipDal, Guid>
{
    
}