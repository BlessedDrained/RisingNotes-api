using Dal.Context;
using MainLib.Dal.Repository.Base;

namespace Dal.MusicClip.Repository;

/// <inheritdoc cref="IClipRepository"/>
public class ClipRepository : Repository<ClipDal, Guid>, IClipRepository
{
    public ClipRepository(ApplicationContext context) : base(context)
    {
    }
}