using Dal.Author;
using MainLib.Dal.Model.Base;

namespace Dal.ShortVideo;

/// <summary>
/// Модель для короткого видео
/// </summary>
public record ShortVideoDal : DalModel<Guid>
{
    /// <summary>
    /// Название
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Идентификатор файла превью
    /// </summary>
    public Guid PreviewFileId { get; set; }
    
    /// <summary>
    /// Идентификатор видео
    /// </summary>
    public Guid VideoFileId { get; set; }
    
    /// <summary>
    /// Идентификатор песни, для которой снято короткое видео. 
    /// </summary>
    /// <remarks>Может быть пустым</remarks>
    public Guid? RelatedSongId { get; set; }
    
    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Продолжительность в миллисекундах
    /// </summary>
    public int DurationMsec { get; set; }
    
    /// <summary>
    /// Идентификатор кто загрузил видео
    /// </summary>
    public Guid UploaderId { get; set; }
    
    /// <summary>
    /// Кто загрузил видео
    /// </summary>
    public AuthorDal Uploader { get; set; }
}