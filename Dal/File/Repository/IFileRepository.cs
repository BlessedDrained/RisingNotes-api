using MainLib.Dal.Repository.Base;

namespace Dal.File.Repository;

/// <summary>
/// Репозиторий для <see cref="FileDal"/>
/// </summary>
public interface IFileRepository : IRepository<FileDal, Guid>
{
}