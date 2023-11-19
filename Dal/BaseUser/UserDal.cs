using Dal.Comment;
using Dal.File;
using Dal.Playlist;
using Dal.Song;
using MainLib.Dal.Model.Base;
using RisingNotesLib.Enums;
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
    /// Пол
    /// </summary>
    public Gender Gender { get; set; }

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
    public List<CommentDal> CommentList { get; set; } = new();

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