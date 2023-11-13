using Dal.BaseUser;
using Dal.Song;
using MainLib.Dal.Model.Base;

namespace Dal.Author;

/// <summary>
/// Автор трека
/// </summary>
public record AuthorDal : DalModel<Guid>
{
    /// <summary>
    /// Имя автора
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Список песен
    /// </summary>
    public List<SongDal> SongList { get; set; }
    
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