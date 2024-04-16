using Dal.Context;
using MainLib.Dal.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Dal.MusicClip.Repository;

/// <inheritdoc cref="IMusicClipRepository"/>
public class MusicClipRepository : Repository<MusicClipDal, Guid>, IMusicClipRepository
{
    public MusicClipRepository(ApplicationContext context) : base(context)
    {
    }
}