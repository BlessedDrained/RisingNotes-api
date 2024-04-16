using Dal.ShortVideo;
using Dal.ShortVideoComment;

namespace Logic.ShortVideoComment;

/// <summary>
/// Менеджер для <see cref="ShortVideoCommentDal"/>
/// </summary>
public interface IShortVideoCommentManager
{
    /// <summary>
    /// Создать клип
    /// </summary>
    Task<Guid> CreateAsync(ShortVideoCommentDal clip);

    /// <summary>
    /// Получить по id
    /// </summary>
    Task<ShortVideoCommentDal> GetAsync(Guid id);

    /// <summary>
    /// Обновить
    /// </summary>
    Task UpdateAsync(ShortVideoCommentDal clip);

    /// <summary>
    /// Удалить
    /// </summary>
    Task DeleteAsync(Guid id);
}