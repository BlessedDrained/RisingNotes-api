using Dal.Song;
using MainLib.Dal.Repository.Base;

namespace Dal.BaseUser.Repository;

/// <summary>
/// Общий репозиторий для пользователей всех типов
/// </summary>
public interface IUserRepository : IRepository<UserDal, Guid>
{
    /// <summary>
    /// Получить идентификатор пользователя из identity по его обычному guid
    /// </summary>
    Task<Guid> GetIdentityUserGuid(Guid userId);

    /// <summary>
    /// Получить обычный идентификатор пользователя по его guid из identity
    /// </summary>
    Task<Guid> GetUserIdByIdentityUserGuid(Guid identityUserId);

    /// <summary>
    /// Получить список информации об избранных треках
    /// </summary>
    Task<List<SongDal>> GetFavoriteSongInfoListAsync(Guid userId);

    /// <summary>
    /// Получить пользователя, включая список его избранных треков
    /// </summary>
    Task<UserDal> GetWithFavoriteSongListAsync(Guid userId);

    /// <summary>
    /// Получить список идентификаторов авторов, на которых подписан пользователь
    /// </summary>
    Task<List<Guid>> GetSubscriptionListAsync(Guid userId);

    /// <summary>
    /// Получить пользователя со списком подписок
    /// </summary>
    Task<UserDal> GetWithSubscriptionListAsync(Guid userId);

    /// <summary>
    /// Получить пользователя со списком исключенных треков
    /// </summary>
    Task<UserDal> GetWithExcludedListAsync(Guid userId);

    /// <summary>
    /// Получить список исключенных треков
    /// </summary>
    Task<List<SongDal>> GetExcludedTrackListAsync(Guid userId);
}