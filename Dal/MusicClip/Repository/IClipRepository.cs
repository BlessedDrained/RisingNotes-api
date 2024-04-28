using MainLib.Dal.Repository.Base;

namespace Dal.MusicClip.Repository;

/// <summary>
/// Репозиторий для <see cref="ClipDal"/>
/// </summary>
public interface IClipRepository : IRepository<ClipDal, Guid>
{
    
}