using MainLib.Dal.Repository.Base;
using RisingNotesLib.Models;

namespace Dal.SongPublish.Repository;

public interface ISongPublishRequestRepository : IRepository<SongPublishRequestDal, Guid>
{
    // /// <summary>
    // /// Получить полную модель
    // /// </summary>
    // Task<SongPublishRequestDal> GetFullAsync(Guid id);

    /// <summary>
    /// Получить список по фильтрам
    /// </summary>
    Task<List<SongPublishRequestDal>> GetListAsync(GetPublishRequestListFilterModel filter, Guid authorId);
}