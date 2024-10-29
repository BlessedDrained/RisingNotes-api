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

    /// <summary>
    /// Имя пользователя уже занято
    /// </summary>
    public const int UserNameIsAlreadyTaken = 2011;

    /// <summary>
    /// В плейлисте нет такого трека
    /// </summary>
    public const int PlaylistHasNoSuchTrack = 2012;

    /// <summary>
    /// Данный трек уже добавлен в плейлист
    /// </summary>
    public const int TrackIsAlreadyInPlaylistException = 2013;

    /// <summary>
    /// Плейлист не принадлежит данному пользователю
    /// </summary>
    public const int PlaylistDoesNotBelongToCurrentUser = 2014;

    /// <summary>
    /// В заявке на загрузку песни нет логотипа
    /// </summary>
    public const int SongPublishRequestHasNoLogo = 2015;

    /// <summary>
    /// В заявке на загрузку песни нет песни
    /// </summary>
    public const int SongPublishRequestHasNoFile = 2016;

    /// <summary>
    /// Текущий статус заявки не позволяет вносить изменения пользователю
    /// </summary>
    public const int CurrentSongPublishRequestStatusDoesNotAllowUserInteraction = 2017;

    /// <summary>
    /// Клип уже был загружен для данной песни
    /// </summary>
    public const int ClipHasAlreadyBeenLoadedForTheSongException = 2018;

    /// <summary>
    /// Данный клип не принадлежит данному автору
    /// </summary>
    public const int ClipDoesNotBelongToCurrentAuthor = 2019;
}