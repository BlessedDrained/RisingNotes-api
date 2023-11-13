using Dal.Context;
using MainLib.Dal.Repository.Base;

namespace Dal.File.Repository;

/// <inheritdoc cref="IFileRepository"/>
public class FileRepository : Repository<FileDal, Guid>, IFileRepository
{
    public FileRepository(ApplicationContext context) : base(context)
    {
    }
}