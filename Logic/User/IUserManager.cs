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
}