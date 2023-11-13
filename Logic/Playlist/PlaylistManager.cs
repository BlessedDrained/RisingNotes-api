using Dal.File;
using Dal.Playlist;
using Dal.Playlist.Repository;
using Dal.Song;
using Dal.Song.Repository;
using Logic.File;
using MainLib.Logging;

namespace Logic.Playlist;

/// <inheritdoc />
public class PlaylistManager : IPlaylistManager
{
    private readonly IPlaylistRepository _repository;
    private readonly ISongRepository _songRepository;
    private readonly IFileManager _fileManager;

    public PlaylistManager(
        IPlaylistRepository repository,
        ISongRepository songRepository,
        IFileManager fileManager)
    {
        _repository = repository;
        _songRepository = songRepository;
        _fileManager = fileManager;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(PlaylistDal playlist)
    {
        using var log = new MethodLog(playlist);

        var id = await _repository.InsertAsync(playlist);

        log.ReturnsValue(id);
        return id;
    }

    /// <inheritdoc />
    public async Task<PlaylistDal> GetInfoAsync(Guid playlistId)
    {
        using var log = new MethodLog(playlistId);
        var playlist = await _repository.GetAsync(playlistId);

        log.ReturnsValue(playlistId);
        return playlist;
    }

    /// <inheritdoc />
    public async Task<List<PlaylistDal>> GetUserPlaylistInfoList(Guid userId)
    {
        using var log = new MethodLog(userId);

        var playlistInfoList = await _repository.GetListAsync(x => x.CreatorId == userId);

        log.ReturnsValue(playlistInfoList);
        return playlistInfoList;
    }

    /// <inheritdoc />
    public async Task<List<SongDal>> GetSongListAsync(Guid playlistId)
    {
        using var log = new MethodLog(playlistId);

        var playlistWithSongList = await _repository.GetWithSongListAsync(playlistId);

        log.ReturnsValue(playlistWithSongList.SongList);
        return playlistWithSongList.SongList;
    }

    /// <inheritdoc />
    public async Task AddTrackAsync(Guid playlistId, Guid songId)
    {
        using var methodLog = new MethodLog(playlistId, songId);

        var playlist = await _repository.GetAsync(playlistId);
        var song = await _songRepository.GetAsync(songId);

        playlist.SongList.Add(song);

        await _repository.UpdateAsync(playlist);
    }

    /// <inheritdoc />
    public async Task<FileDal> GetLogoAsync(Guid playlistId)
    {
        using var log = new MethodLog(playlistId);

        var playlist = await _repository.GetAsync(playlistId);
        var logo = await _fileManager.DownloadAsync(playlist.LogoFileId.Value);

        log.ReturnsValue(logo);
        return logo;
    }
}