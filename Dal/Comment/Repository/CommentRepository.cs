using Dal.Context;
using MainLib.Dal.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Dal.Comment.Repository;

/// <inheritdoc cref="ICommentRepository"/>
public class CommentRepository : Repository<CommentDal, Guid>, ICommentRepository
{
    public CommentRepository(ApplicationContext context) : base(context)
    {
    }

    /// <inheritdoc />
    public async Task<List<CommentDal>> GetSongCommentListAsync(Guid songId)
    {
        var commentList = await Set
            .Where(x => x.SongId == songId)
            .ToListAsync();

        return commentList;
    }
}