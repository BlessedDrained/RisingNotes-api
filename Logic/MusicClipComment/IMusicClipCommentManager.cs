using Dal.MusicClip;
using Dal.MusicClipComment;

namespace Logic.MusicClipComment;

/// <summary>
/// Менеджер для <see cref="MusicClipCommentDal"/>
/// </summary>
public interface IMusicClipCommentManager
{
    /// <summary>
    /// Создать клип
    /// </summary>
    Task<Guid> CreateAsync(MusicClipCommentDal clip);

    /// <summary>
    /// Получить по id
    /// </summary>
    Task<MusicClipCommentDal> GetAsync(Guid id);

    /// <summary>
    /// Обновить
    /// </summary>
    Task UpdateAsync(MusicClipCommentDal clip);

    /// <summary>
    /// Удалить
    /// </summary>
    Task DeleteAsync(Guid id);
}