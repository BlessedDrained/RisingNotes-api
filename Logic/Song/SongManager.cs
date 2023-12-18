using Dal.BaseUser.Repository;
using Dal.File;
using Dal.Song;
using Dal.Song.Repository;
using Logic.File;
using MainLib.Logging;
using MainLib.TagLib;
using RisingNotesLib.Exceptions;

namespace Logic.Song;

/// <summary>
/// Менеджер для <see cref="SongDal"/>
/// </summary>
public class SongManager : ISongManager
{
    private readonly ISongRepository _songRepository;
    private readonly IFileManager _fileManager;
    private readonly IUserRepository _userRepository;

    public SongManager(
        ISongRepository songRepository,
        IFileManager fileManager,
        IUserRepository userRepository)
    {
        _songRepository = songRepository;
        _fileManager = fileManager;
        _userRepository = userRepository;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(SongDal model, FileDal songFile, FileDal logoFile)
    {
        using var log = new MethodLog(model, songFile, logoFile);

        var file = TagLib.File.Create(new FileAbstraction($"{songFile.Name}.{songFile.Extension}", songFile.Content));
        model.DurationMsec = Convert.ToInt32(file.Properties.Duration.TotalMilliseconds);

        var songFileId = await _fileManager.UploadAsync(songFile);
        var logoFileId = await _fileManager.UploadAsync(logoFile);
        model.SongFileId = songFileId;
        model.LogoFileId = logoFileId;

        var songId = await _songRepository.InsertAsync(model);

        log.ReturnsValue(songId);
        return songId;
    }

    /// <inheritdoc />
    public async Task<SongDal> GetSongInfoAsync(Guid songId)
    {
        using var log = new MethodLog(songId);

        var songInfo = await _songRepository.GetWithAuthorAsync(songId);

        log.ReturnsValue(songInfo);
        return songInfo;
    }

    /// <inheritdoc />
    public async Task<List<SongDal>> GetAuthorSongInfoListAsync(Guid authorId)
    {
        using var log = new MethodLog(authorId);

        var songInfoList = await _songRepository.GetListAsync(x => x.AuthorId == authorId);

        log.ReturnsValue(songInfoList);
        return songInfoList;
    }

    /// <inheritdoc />
    public async Task<FileDal> GetSongFileAsync(Guid songId)
    {
        using var log = new MethodLog(songId);

        var song = await _songRepository.GetAsync(songId);
        var file = await _fileManager.DownloadAsync(song.SongFileId);

        // добавляем прослушивание при получении
        song.AuditionCount++;
        await _songRepository.UpdateAsync(song);

        log.ReturnsValue(file);
        return file;
    }

    /// <inheritdoc />
    public async Task<FileDal> GetSongLogoAsync(Guid songId)
    {
        using var log = new MethodLog(songId);

        var song = await _songRepository.GetAsync(songId);

        if (!song.LogoFileId.HasValue)
        {
            throw new SongHasNoLogoException(songId);
        }

        var file = await _fileManager.DownloadAsync(song.LogoFileId.Value);

        log.ReturnsValue(file);
        return file;
    }

    /// <inheritdoc />
    public Task UpdateAsync(SongDal model)
    {
        using var log = new MethodLog(model);
        return _songRepository.UpdateAsync(model);
    }

    /// <inheritdoc />
    public Task DeleteAsync(Guid id)
    {
        using var log = new MethodLog(id);
        return _songRepository.DeleteAsync(id);
    }

    /// <inheritdoc />
    public async Task<List<SongDal>> GetFavoriteSongInfoListAsync(Guid userId)
    {
        using var log = new MethodLog(userId);

        var favoriteSongInfoList = await _userRepository.GetFavoriteSongInfoListAsync(userId);

        log.ReturnsValue(favoriteSongInfoList);
        return favoriteSongInfoList;
    }

    /// <inheritdoc />
    public async Task AddFavoriteAsync(Guid userId, Guid songId)
    {
        using var log = new MethodLog(userId, songId);
        var song = await _songRepository.GetAsync(songId);

        var user = await _userRepository.GetWithFavoriteSongListAsync(userId);
        if (user.FavoriteSongList.Exists(x => x.Id == songId))
        {
            throw new TrackIsAlreadyInFavoriteException(songId);
        }

        user.FavoriteSongList.Add(song);

        await _userRepository.UpdateAsync(user);
    }

    /// <inheritdoc />
    public async Task RemoveFavoriteAsync(Guid userId, Guid songId)
    {
        using var log = new MethodLog(userId, songId);
        var user = await _userRepository.GetWithFavoriteSongListAsync(userId);

        var song = user.FavoriteSongList.FirstOrDefault(x => x.Id == songId);
        if (song == null)
        {
            throw new NoSuchFavoriteTrackException(songId);
        }

        user.FavoriteSongList.Remove(song);

        await _userRepository.UpdateAsync(user);
    }

    public async Task UpdateLogoAsync(Guid authorId, Guid songId, FileDal file)
    {
        using var log = new MethodLog(authorId, songId, file);

        var song = await _songRepository.GetAsync(songId);

        if (song.AuthorId != authorId)
        {
            throw new Exception($"Author with id={authorId} is not an author of track with id={songId}");
        }

        // if (song.LogoFileId.HasValue)
        // {
        //     await _fileManager.DeleteAsync(song.LogoFileId.Value);
        // }

        var fileId = await _fileManager.UploadAsync(file);

        song.LogoFile = file;
        song.LogoFileId = fileId;

        await _songRepository.UpdateAsync(song);
    }

    /// <inheritdoc />
    public async Task<int> GetAuditionCountAsync(Guid songId)
    {
        using var log = new MethodLog(songId);
        var song = await _songRepository.GetAsync(songId);

        return song.AuditionCount;
    }

    /// <inheritdoc />
    public async Task ExcludeAsync(Guid userId, Guid songId)
    {
        using var log = new MethodLog(userId, songId);

        var user = await _userRepository.GetAsync(userId);
        var song = await _songRepository.GetAsync(songId);

        user.ExcludedSongList.Add(song);

        await _userRepository.UpdateAsync(user);
    }


    /// <inheritdoc />
    public async Task RemoveFromExcludedAsync(Guid userId, Guid songId)
    {
        using var log = new MethodLog(userId, songId);

        var user = await _userRepository.GetWithExcludedListAsync(userId);
        var song = await _songRepository.GetAsync(songId);

        user.ExcludedSongList.Remove(song);

        await _userRepository.UpdateAsync(user);
    }
}