using Dal.BaseUser;
using MainLib.Dal.Model.Base;

namespace Dal.ShortVideoComment;

/// <summary>
/// Комментарий к короткому видео
/// </summary>
public record ShortVideoCommentDal : DalModel<Guid>
{
    /// <summary>
    /// Идентификатор автора
    /// </summary>
    public Guid AuthorId { get; init; }
    
    /// <summary>
    /// Автор
    /// </summary>
    public UserDal Author { get; init; }
    
    /// <summary>
    /// Текст комментария
    /// </summary>
    public string Text { get; init; }
    
    /// <summary>
    /// Идентификатор видео
    /// </summary>
    public Guid ShortVideoId { get; init; }
}