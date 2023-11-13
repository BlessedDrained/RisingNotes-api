using Dal.Comment;

namespace Logic.SongComment;

/// <summary>
/// Менеджер комментариев к песням
/// </summary>
public interface ISongCommentManager
{
    /// <summary>
    /// Добавить комментарий
    /// </summary>
    Task<Guid> AddCommentAsync(Guid songId, Guid userId, string commentText);

    /// <summary>
    /// Получить список комментариев к песне
    /// </summary>
    Task<List<CommentDal>> GetCommentListAsync(Guid songId);

    /// <summary>
    /// Изменить комментарий
    /// </summary>
    Task EditCommentAsync(Guid commentId, string newText);

    /// <summary>
    /// Удалить комментарий
    /// </summary>
    Task RemoveCommentAsync(Guid commentId);
}