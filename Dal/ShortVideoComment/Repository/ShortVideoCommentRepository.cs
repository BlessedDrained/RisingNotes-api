using Dal.Context;
using MainLib.Dal.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Dal.ShortVideoComment.Repository;

public class ShortVideoCommentRepository : Repository<ShortVideoCommentDal, Guid>, IShortVideoCommentRepository
{
    public ShortVideoCommentRepository(ApplicationContext context) : base(context)
    {
    }
}