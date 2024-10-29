using Dal.Author;
using Dal.File;
using Dal.Song;
using MainLib.Dal.Model.Base;
using RisingNotesLib.Enums;

namespace Dal.SongPublish;

/// <summary>
/// Заявка на публикацию песни
/// </summary>
public record SongPublishRequestDal : DalModel<Guid>
{
    /// <summary>
    /// Статус заявки
    /// </summary>
    public PublishRequestStatus Status { get; set; }
    
    /// <summary>
    /// Комментарий ревьюера
    /// </summary>
    public string ReviewerComment { get; set; }

    /// <summary>
    /// Идентификатор автора
    /// </summary>
    public Guid? AuthorId { get; set; }
    
    /// <summary>
    /// Нав свойство
    /// </summary>
    public AuthorDal Author { get; set; }
    
    /// <summary>
    /// Нав свойство
    /// </summary>
    /// <remarks>Задается после публикации</remarks>
    public SongDal Song { get; set; }
    
    /// <summary>
    /// Нав свойство
    /// </summary>
    /// <remarks>Задается после публикации</remarks>
    public Guid? SongId { get; set; }

    /// <summary>
    /// Название трека
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Текст песни
    /// </summary>
    public string Lyrics { get; set; }
    
    /// <summary>
    /// Является ли инструментальной
    /// </summary>
    public bool Instrumental { get; set; }
    
    /// <summary>
    /// Продолжительность трека
    /// </summary>
    public int DurationMs { get; set; }
    
    /// <summary>
    /// Список жанров песни
    /// </summary>
    public string[] GenreList { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Список настроений
    /// </summary>
    public string[] VibeList { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Список языков
    /// </summary>
    public string[] LanguageList { get; set; } = Array.Empty<string>();

    /// <summary>
    /// 
    /// </summary>
    public Gender[] VocalGenderList { get; set; } = Array.Empty<Gender>();

    /// <summary>
    /// Идентификатор файла трека
    /// </summary>
    public Guid? SongFileId { get; set; }

    /// <summary>
    /// Нав свойство
    /// </summary>
    public FileDal? SongFile { get; set; }

    /// <summary>
    /// Нав свойство
    /// </summary>
    public Guid? LogoFileId { get; set; }

    /// <summary>
    /// Нав свойство
    /// </summary>
    public FileDal? LogoFile { get; set; }
}