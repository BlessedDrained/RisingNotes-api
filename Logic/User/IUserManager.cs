using Dal.BaseUser;
using Dal.File;

namespace Logic.User;

///
public interface IUserManager
{
    /// <summary>
    /// Создать пользователя
    /// </summary>
    Task<Guid> CreateAsync(UserDal user);

    /// <summary>
    /// Получить лого пользователя
    /// </summary>
    Task<FileDal> GetLogoAsync(Guid userId);

    /// <summary>
    /// Подписаться на автора
    /// </summary>
    Task SubscribeAsync(Guid userId, Guid authorId);

    /// <summary>
    /// Отписаться от автора
    /// </summary>
    Task UnsubscribeAsync(Guid userId, Guid authorId);

    /// <summary>
    /// Обновить логотип пользователя
    /// </summary>
    Task UpdateLogoAsync(Guid userId, FileDal file);

    /// <summary>
    /// Обновить имя пользователя
    /// </summary>
    Task UpdateUserNameAsync(Guid userId, string userName);
}