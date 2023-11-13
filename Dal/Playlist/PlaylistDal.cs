using Dal.BaseUser;
using Dal.File;
using Dal.Song;
using MainLib.Dal.Model.Base;

namespace Dal.Playlist;

/// <summary>
/// Модель плейлиста
/// </summary>
public record PlaylistDal : DalModel<Guid>
{
    /// <summary>
    /// Идентификатор создателя
    /// </summary>
    public Guid CreatorId { get; set; }

    /// <summary>
    /// Навигационное свойство
    /// </summary>
    public UserDal Creator { get; set; }

    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Является ли приватным
    /// </summary>
    public bool IsPrivate { get; set; }

    /// <summary>
    /// Список треков в плейлисте
    /// </summary>
    public List<SongDal> SongList { get; set; } = new();

    /// <summary>
    /// нав свойство
    /// </summary>
    public Guid? LogoFileId { get; set; }

    /// <summary>
    /// нав свойство
    /// </summary>
    public FileDal LogoFile { get; set; }
}