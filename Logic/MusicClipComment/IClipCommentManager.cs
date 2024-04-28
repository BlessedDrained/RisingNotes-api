using Dal.MusicClipComment;

namespace Logic.MusicClipComment;

/// <summary>
/// Менеджер для <see cref="ClipCommentDal"/>
/// </summary>
public interface IClipCommentManager
{
    /// <summary>
    /// Добавить комментарий
    /// </summary>
    Task<Guid> AddCommentAsync(Guid songId, Guid userId, string commentText);

    /// <summary>
    /// Получить список комментариев к песне
    /// </summary>
    Task<List<ClipCommentDal>> GetCommentListAsync(Guid songId);

    /// <summary>
    /// Изменить комментарий
    /// </summary>
    Task EditCommentAsync(Guid commentId, string newText);

    /// <summary>
    /// Удалить комментарий
    /// </summary>
    Task RemoveCommentAsync(Guid commentId);
}