using Dal.BaseUser;
using Dal.Song;
using MainLib.Dal.Model.Base;

namespace Dal.SongComment;

/// <summary>
/// Комментарий к песне
/// </summary>
public record SongCommentDal : DalModel<Guid>
{
    /// <summary>
    /// Текст
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Нав свойство
    /// </summary>
    public UserDal Creator { get; set; }

    /// <summary>
    /// Нав свойство
    /// </summary>
    public Guid CreatorId { get; set; }
    
    /// <summary>
    /// Нав свойство
    /// </summary>
    public SongDal Song { get; set; }
    
    /// <summary>
    /// Нав свойство
    /// </summary>
    public Guid SongId { get; set; }
}