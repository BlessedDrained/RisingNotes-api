using Dal.Author;
using Dal.File;
using Dal.MusicClipComment;
using Dal.Playlist;
using Dal.ShortVideoComment;
using Dal.Song;
using Dal.SongComment;
using MainLib.Dal.Model.Base;
using RisingNotesLib.Models;

namespace Dal.BaseUser;

/// <summary>
/// базовая модель для всех типов пользователей
/// </summary>
public record UserDal : DalModel<Guid>
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Избранные треки
    /// </summary>
    public List<SongDal> FavoriteSongList { get; set; } = new();

    /// <summary>
    /// Список плейлистов пользователя
    /// </summary>
    public List<PlaylistDal> PlaylistList { get; set; } = new();

    /// <summary>
    /// Список комментариев, оставленных пользователем
    /// </summary>
    public List<SongCommentDal> SongCommentList { get; set; } = new();

    /// <summary>
    /// Список комментариев к клипам
    /// </summary>
    public List<ClipCommentDal> MusicClipCommentList { get; set; } = new();

    /// <summary>
    /// Список комментариев к коротким видео
    /// </summary>
    public List<ShortVideoCommentDal> ShortVideoCommentList { get; set; } = new();

    /// <summary>
    /// Список подписок
    /// </summary>
    public List<AuthorDal> SubscriptionList { get; set; } = new();

    /// <summary>
    /// Список исключенных треков
    /// </summary>
    public List<SongDal> ExcludedSongList { get; set; } = new();

    /// <summary>
    /// Нав свойство
    /// </summary>
    public FileDal LogoFile { get; set; }

    /// <summary>
    /// Нав свойство
    /// </summary>
    public Guid? LogoFileId { get; set; }

    /// <summary>
    /// Является ли музыкантом
    /// </summary>
    public bool IsAuthor { get; set; }

    /// <summary>  
    /// Нав свойство
    /// </summary>
    public AppIdentityUser IdentityUser { get; set; }

    /// <summary>
    /// Нав свойство
    /// </summary>
    public string IdentityUserId { get; set; }
}