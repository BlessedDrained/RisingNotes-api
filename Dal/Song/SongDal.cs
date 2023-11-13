using Dal.Author;
using Dal.BaseUser;
using Dal.Comment;
using Dal.File;
using Dal.Playlist;
using MainLib.Dal.Model.Base;

namespace Dal.Song;

/// <summary>
/// Модель трека
/// </summary>
public record SongDal : DalModel<Guid>
{
    /// <summary>
    /// Автор
    /// </summary>
    public AuthorDal Author { get; set; }

    /// <summary>
    /// Идентификатор автора
    /// </summary>
    public Guid AuthorId { get; set; }

    /// <summary>
    /// Название трека
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Продолжительность трека в миллисекундах
    /// </summary>
    public double DurationMsec { get; set; }

    /// <summary>
    /// Текст песни
    /// </summary>
    public string Lyrics { get; set; }

    /// <summary>
    /// Идентификатор файла трека
    /// </summary>
    public Guid SongFileId { get; set; }

    /// <summary>
    /// Нав свойство
    /// </summary>
    public FileDal SongFile { get; set; }

    /// <summary>
    /// Нав свойство
    /// </summary>
    public Guid? LogoFileId { get; set; }

    /// <summary>
    /// Нав свойство
    /// </summary>
    public FileDal LogoFile { get; set; }

    /// <summary>
    /// Нав свойство список плейлистов
    /// </summary>
    public List<PlaylistDal> PlaylistList { get; set; } = new();

    /// <summary>
    /// Нав свойство список людей, которые добавили трек в избранное
    /// </summary>
    public List<UserDal> AddedToFavoriteUserList { get; set; } = new();

    /// <summary>
    /// Список комментариев
    /// </summary>
    public List<CommentDal> CommentList { get; set; } = new();
}