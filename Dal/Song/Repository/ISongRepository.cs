using MainLib.Dal.Repository.Base;
using RisingNotesLib.Models;

namespace Dal.Song.Repository;

/// <summary>
/// Репозиторий для <see cref="SongDal"/>
/// </summary>
public interface ISongRepository : IRepository<SongDal, Guid>
{
    /// <summary>
    /// Получить полную информацию о песне
    /// </summary>
    /// <returns></returns>
    Task<SongDal> GetFullAsync(Guid songId);

    /// <summary>
    /// Получить список треков по фильтрам
    /// </summary>
    Task<List<SongDal>> GetListAsync(GetSongListFilterModel filter);
}