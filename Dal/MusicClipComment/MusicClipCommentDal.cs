using Dal.BaseUser;
using MainLib.Dal.Model.Base;

namespace Dal.MusicClipComment;

/// <summary>
/// Комментарий к клипу
/// </summary>
public record MusicClipCommentDal : DalModel<Guid>
{
    /// <summary>
    /// Текст комментария
    /// </summary>
    public string Text { get; set; }
    
    /// <summary>
    /// Идентификатор автора
    /// </summary>
    public Guid AuthorId { get; set; }
    
    /// <summary>
    /// Модель автора
    /// </summary>
    public UserDal Author { get; set; }
    
    /// <summary>
    /// Идентификатор песни, для которой загружен клип
    /// </summary>
    public Guid SongId { get; set; }
    
    /// <summary>
    /// Идентификатор файла превью
    /// </summary>
    public Guid PreviewFileId { get; set; }
}