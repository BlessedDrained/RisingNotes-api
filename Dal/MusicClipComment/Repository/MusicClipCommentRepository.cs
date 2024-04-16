using Dal.Context;
using MainLib.Dal.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Dal.MusicClipComment.Repository;

/// <inheritdoc cref="IMusicClipCommentRepository"/>
public class MusicClipCommentRepository : Repository<MusicClipCommentDal, Guid>, IMusicClipCommentRepository
{
    public MusicClipCommentRepository(ApplicationContext context) : base(context)
    {
    }
}