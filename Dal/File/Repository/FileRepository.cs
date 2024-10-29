using Dal.Context;
using MainLib.Dal.Repository.Base;
using MainLib.Enums;
using Microsoft.EntityFrameworkCore;

namespace Dal.File.Repository;

/// <inheritdoc cref="IFileRepository"/>
public class FileRepository : Repository<FileDal, Guid>, IFileRepository
{
    public FileRepository(ApplicationContext context) : base(context)
    {
    }

    /// <inheritdoc />
    [Obsolete(null, error: true)]
    public async Task<List<Guid>> GetAllDbStoredIdListAsync()
    {
        var listAsync = await Set
            .Where(x => x.StorageType == StorageType.Database)
            .Select(x => x.Id)
            .ToListAsync();

        return listAsync;
    }
}