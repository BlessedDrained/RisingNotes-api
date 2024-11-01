﻿using Dal.ShortVideoComment;

namespace Logic.ShortVideoComment;

/// <summary>
/// Менеджер для <see cref="ShortVideoCommentDal"/>
/// </summary>
public interface IShortVideoCommentManager
{
    /// <summary>
    /// Добавить комментарий
    /// </summary>
    Task<Guid> AddCommentAsync(Guid songId, Guid userId, string commentText);

    /// <summary>
    /// Получить список комментариев к песне
    /// </summary>
    Task<List<ShortVideoCommentDal>> GetCommentListAsync(Guid songId);

    /// <summary>
    /// Изменить комментарий
    /// </summary>
    Task EditCommentAsync(Guid commentId, string newText);

    /// <summary>
    /// Удалить комментарий
    /// </summary>
    Task RemoveCommentAsync(Guid commentId);
}