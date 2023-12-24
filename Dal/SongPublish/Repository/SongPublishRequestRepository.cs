using Dal.Context;
using MainLib.Dal.Repository.Base;
using Microsoft.EntityFrameworkCore;
using RisingNotesLib.Models;

namespace Dal.SongPublish.Repository;

/// <inheritdoc cref="ISongPublishRequestRepository"/>
public class SongPublishRequestRepository : Repository<SongPublishRequestDal, Guid>, ISongPublishRequestRepository
{
    public SongPublishRequestRepository(ApplicationContext context) : base(context)
    {
    }

    // Закомментирован, т.к файлы по особенному получаются через файл мнееджер
    // /// <inheritdoc />
    // public async Task<SongPublishRequestDal> GetFullAsync(Guid id)
    // {
    //     var result = await Set
    //         .Where(x => x.Id == id)
    //         .Include(x => x.LogoFile)
    //         .Include(x => x.SongFile)
    //         .SingleOrDefaultAsync();
    //
    //     if (result == null)
    //     {
    //         throw new EntityNotFoundException<SongPublishRequestDal>(id);
    //     }
    //
    //     return result;
    // }

    /// <inheritdoc />
    public Task<List<SongPublishRequestDal>> GetListAsync(GetPublishRequestListFilterModel filter, Guid authorId)
    {
        var result = Set.AsQueryable();
        
        result = result.Include(x => x.Author);
        result = result.Where(x => x.AuthorId == authorId);

        if (filter.OrderByStatusDescending.HasValue)
        {
            result = filter.OrderByStatusDescending.Value
                ? result.OrderByDescending(x => x.Status)
                : result.OrderBy(x => x.Status);
        }

        if (filter.OrderByAuthorNameDescending.HasValue)
        {
            result = filter.OrderByAuthorNameDescending.Value
                ? result.OrderByDescending(x => x.Status)
                : result.OrderBy(x => x.Status);
        }

        if (filter.Offset.HasValue)
        {
            result = result.Skip(filter.Offset.Value);
        }

        if (filter.Count.HasValue)
        {
            result = result.Take(filter.Count.Value);
        }

        return result.ToListAsync();
    }
}