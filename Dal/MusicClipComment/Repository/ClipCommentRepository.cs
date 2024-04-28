using Dal.Context;
using MainLib.Dal.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Dal.MusicClipComment.Repository;

/// <inheritdoc cref="IClipCommentRepository"/>
public class ClipCommentRepository : Repository<ClipCommentDal, Guid>, IClipCommentRepository
{
    public ClipCommentRepository(ApplicationContext context) : base(context)
    {
    }

    public async Task<List<ClipCommentDal>> GetClipCommentListAsync(Guid songId)
    {
        var commentList = await Set
            .Where(x => x.SongId == songId)
            .ToListAsync();

        return commentList;
    }
}