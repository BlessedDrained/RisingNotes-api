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
    public Guid CreatorId { get; set; }
    
    /// <summary>
    /// Автор
    /// </summary>
    public UserDal Creator { get; set; }
    
    /// <summary>
    /// Текст комментария
    /// </summary>
    public string Text { get; set; }
    
    /// <summary>
    /// Идентификатор видео
    /// </summary>
    public Guid ShortVideoId { get; set; }
}