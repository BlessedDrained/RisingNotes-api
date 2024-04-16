using Dal.ShortVideo;

namespace Logic.ShortVideo;

/// <summary>
/// Менеджер для <see cref="ShortVideoDal"/>
/// </summary>
public interface IShortVideoManager
{
    /// <summary>
    /// Создать клип
    /// </summary>
    Task<Guid> CreateAsync(ShortVideoDal clip);

    /// <summary>
    /// Получить по id
    /// </summary>
    Task<ShortVideoDal> GetAsync(Guid id);

    /// <summary>
    /// Обновить
    /// </summary>
    Task UpdateAsync(ShortVideoDal clip);

    /// <summary>
    /// Удалить
    /// </summary>
    Task DeleteAsync(Guid id);
}