using Dal.BaseUser;
using MainLib.Dal.Model.Base;

namespace Dal.MusicClipComment;

/// <summary>
/// Комментарий к клипу
/// </summary>
public record ClipCommentDal : DalModel<Guid>
{
    /// <summary>
    /// Текст комментария
    /// </summary>
    public string Text { get; set; }
    
    /// <summary>
    /// Идентификатор автора
    /// </summary>
    public Guid CreatorId { get; set; }
    
    /// <summary>
    /// Модель автора
    /// </summary>
    public UserDal Creator { get; set; }
    
    /// <summary>
    /// Идентификатор песни, для которой загружен клип
    /// </summary>
    public Guid SongId { get; set; }
}