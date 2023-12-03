using Dal.File;
using Dal.Song;

namespace Logic.Song;

/// <summary>
/// Менеджер для <see cref="SongDal"/>
/// </summary>
public interface ISongManager
{
    /// <summary>
    /// Создать трек
    /// </summary>
    Task<Guid> CreateAsync(SongDal model, FileDal songFile, FileDal logoFile);

    /// <summary>
    /// Получить информацию о треке без самого файла
    /// </summary>
    Task<SongDal> GetSongInfoAsync(Guid songId);

    /// <summary>
    /// Получить список информации о треках автора
    /// </summary>
    /// <remarks>Включает в себя только основную инфу по типу текста, продолжительности</remarks>
    Task<List<SongDal>> GetAuthorSongInfoListAsync(Guid authorId);

    /// <summary>
    /// Получить сам трек
    /// </summary>
    Task<FileDal> GetSongFileAsync(Guid songId);

    /// <summary>
    /// Получить лого трека
    /// </summary>
    Task<FileDal> GetSongLogoAsync(Guid songId);

    /// <summary>
    /// Обновить трек
    /// </summary>
    Task UpdateAsync(SongDal model);

    /// <summary>
    /// Удалить трек
    /// </summary>
    Task DeleteAsync(Guid id);

    /// <summary>
    /// Получить список информации об избранных треках пользователя
    /// </summary>
    Task<List<SongDal>> GetFavoriteSongInfoListAsync(Guid userId);

    /// <summary>
    /// Добавить трек в избранное
    /// </summary>
    Task AddFavoriteAsync(Guid userId, Guid songId);

    /// <summary>
    /// Удалить трек из избранного
    /// </summary>
    Task RemoveFavoriteAsync(Guid userId, Guid songId);
    
    /// <summary>
    /// Обновить логотип пользователя
    /// </summary>
    Task UpdateLogoAsync(Guid authorId, Guid songId, FileDal file);

    /// <summary>
    /// Получить количество прослуишиваний трека
    /// </summary>
    Task<int> GetAuditionCountAsync(Guid songId);
}