using Dal.Author;
using MainLib.Dal.Model.Base;

namespace Dal.MusicClip;

/// <summary>
/// модель видео
/// </summary>
public record ClipDal : DalModel<Guid>
{
    /// <summary>
    /// Название
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Идентификатор файла с превью видео
    /// </summary>
    public Guid PreviewFileId { get; set; }
    
    /// <summary>
    /// Идентификатор файла клипа
    /// </summary>
    public Guid ClipFileId { get; set; }
    
    /// <summary>
    /// Идентификатор песни, для которой создан клип
    /// </summary>
    public Guid SongId { get; set; }
    
    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Продолжительность в миллисекундах
    /// </summary>
    public int DurationMsec { get; set; } 
    
    /// <summary>
    /// Идентификатор кто загрузил клип
    /// </summary>
    public Guid UploaderId { get; set; }
    
    /// <summary>
    /// Кто загрузил клип
    /// </summary>
    public AuthorDal Uploader { get; set; }
}