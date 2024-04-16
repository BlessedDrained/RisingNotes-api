using Dal.Context;
using MainLib.Dal.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Dal.SongComment.Repository;

/// <inheritdoc cref="ISongCommentRepository"/>
public class SongCommentRepository : Repository<SongCommentDal, Guid>, ISongCommentRepository
{
    public SongCommentRepository(ApplicationContext context) : base(context)
    {
    }

    /// <inheritdoc />
    public async Task<List<SongCommentDal>> GetSongCommentListAsync(Guid songId)
    {
        var commentList = await Set
            .Where(x => x.SongId == songId)
            .ToListAsync();

        return commentList;
    }
}