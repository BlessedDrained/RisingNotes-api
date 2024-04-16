namespace RisingNotesLib.Constant;

/// <summary>
/// Константы для кодов ошибок Rising Notes
/// </summary>
public static class RisingNotesErrorConstants
{
    /// <summary>
    /// Ошибка добавления пользователя к роли
    /// </summary>
    public const int AddToRole = 2000;
    
    /// <summary>
    /// Ошибка загрузки файла
    /// </summary>
    public const int FileUpload = 2001;
    
    /// <summary>
    /// Такого трека нет в избранном
    /// </summary>
    public const int NoSuchFavoriteTrack = 2002;
    
    /// <summary>
    /// У плейлиста нет логотипа
    /// </summary>
    public const int PlaylistHasNoLogo = 2003;
    
    /// <summary>
    /// Трек уже добавлен в избранное
    /// </summary>
    public const int TrackIsAlreadyInFavorite = 2004;
    
    /// <summary>
    /// Пользователь не имеет логотипа
    /// </summary>
    public const int UserHasNoLogo = 2005;
    
    /// <summary>
    /// Ошибка при регистрации пользователя
    /// </summary>
    public const int UserRegistration = 2006;

    /// <summary>
    /// Неверный размер изображения
    /// </summary>
    public const int InvalidImageSize = 2007;

    /// <summary>
    /// У песни нет логотипа
    /// </summary>
    public const int SongHasNoLogo = 2008;

    /// <summary>
    /// Ошибка при загрузке файла
    /// </summary>
    public const int FileDownload = 2009;

    /// <summary>
    /// Ошибка при удалении файла
    /// </summary>
    public const int FileDelete = 2010;
}