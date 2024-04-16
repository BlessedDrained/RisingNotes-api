using Dal.BaseUser;
using Dal.MusicClipComment;
using Dal.ShortVideo;
using Dal.Song;
using MainLib.Dal.Model.Base;

namespace Dal.Author;

/// <summary>
/// Автор трека
/// </summary>
public record AuthorDal : DalModel<Guid>
{
    /// <summary>
    /// Имя автора + имя пользователя по совместительству
    /// </summary>
    public string Name => User.UserName;
    
    /// <summary>
    /// Список песен
    /// </summary>
    public List<SongDal> SongList { get; set; } = new();

    /// <summary>
    /// Список музыкальных клипов
    /// </summary>
    public List<MusicClipCommentDal> MusicClipList { get; set; } = new();

    /// <summary>
    /// Список коротких видео, загруженных автором
    /// </summary>
    public List<ShortVideoDal> ShortVideoList { get; set; } = new();

    /// <summary>
    /// Список подписанных пользователей
    /// </summary>
    public List<UserDal> SubscribedUserList { get; set; } = new();

    /// <summary>
    /// Блок "О себе"
    /// </summary>
    public string About { get; set; }

    /// <summary>
    /// Ссылка на Vk
    /// </summary>
    public string VkLink { get; set; }

    /// <summary>
    /// Ссылка на яндекс музыку
    /// </summary>
    public string YaMusicLink { get; set; }

    /// <summary>
    /// Ссылка на личный сайт
    /// </summary>
    public string WebSiteLink { get; set; }

    /// <summary>
    /// Нав свойство
    /// </summary>
    public UserDal User { get; set; }

    /// <summary>
    /// Нав свойство
    /// </summary>
    public Guid UserId { get; set; }
}