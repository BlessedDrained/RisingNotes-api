﻿using Dal.File;
using Dal.Playlist;
using Dal.Playlist.Repository;
using Dal.Song;
using Dal.Song.Repository;
using Logic.File;
using MainLib.Logging;
using RisingNotesLib.Exceptions;

namespace Logic.Playlist;

/// <inheritdoc />
public class PlaylistManager : IPlaylistManager
{
    private readonly IPlaylistRepository _playlistRepository;
    private readonly ISongRepository _songRepository;
    private readonly IFileManager _fileManager;

    public PlaylistManager(
        IPlaylistRepository playlistRepository,
        ISongRepository songRepository,
        IFileManager fileManager)
    {
        _playlistRepository = playlistRepository;
        _songRepository = songRepository;
        _fileManager = fileManager;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(PlaylistDal playlist)
    {
        using var log = new MethodLog(playlist);

        var id = await _playlistRepository.InsertAsync(playlist);

        log.ReturnsValue(id);
        return id;
    }

    /// <inheritdoc />
    public async Task<PlaylistDal> GetInfoAsync(Guid playlistId)
    {
        using var log = new MethodLog(playlistId);
        var playlist = await _playlistRepository.GetAsync(playlistId);

        log.ReturnsValue(playlistId);
        return playlist;
    }

    /// <inheritdoc />
    public async Task<List<PlaylistDal>> GetUserPlaylistInfoList(Guid userId)
    {
        using var log = new MethodLog(userId);

        var playlistInfoList = await _playlistRepository.GetListAsync(x => x.CreatorId == userId);

        log.ReturnsValue(playlistInfoList);
        return playlistInfoList;
    }

    /// <inheritdoc />
    public async Task<List<SongDal>> GetSongListAsync(Guid playlistId)
    {
        using var log = new MethodLog(playlistId);

        var playlistWithSongList = await _playlistRepository.GetWithSongListAsync(playlistId);

        log.ReturnsValue(playlistWithSongList.SongList);
        return playlistWithSongList.SongList;
    }

    /// <inheritdoc />
    public async Task AddTrackAsync(Guid playlistId, Guid songId, Guid userId)
    {
        using var methodLog = new MethodLog(playlistId, songId);

        var playlist = await _playlistRepository.GetWithSongListAsync(playlistId);

        if (playlist.CreatorId != userId)
        {
            throw new PlaylistDoesNotBelongToCurrentUser(playlistId, userId);
        }
        
        if (playlist.SongList?.Exists(x => x.Id == songId) == true)
        {
            throw new TrackIsAlreadyInPlaylistException(songId, playlistId);
        }
        
        var song = await _songRepository.GetAsync(songId);

        playlist.SongList?.Add(song);

        await _playlistRepository.UpdateAsync(playlist);
    }

    /// <inheritdoc />
    public async Task RemoveTrackAsync(Guid playlistId, Guid songId)
    {
        using var methodLog = new MethodLog(playlistId, songId);

        var playlist = await _playlistRepository.GetWithSongListAsync(playlistId);
        var song = await _songRepository.GetAsync(songId);

        var playlistSong = playlist.SongList.FirstOrDefault(x => x.Id == song.Id);
        if (playlistSong == null)
        {
            throw new PlaylistHasNoSuchTrackException(playlistId, songId);
        }

        playlist.SongList.Remove(playlistSong);

        await _playlistRepository.UpdateAsync(playlist);
    }

    /// <inheritdoc />
    public async Task<FileDal> GetLogoAsync(Guid playlistId)
    {
        using var log = new MethodLog(playlistId);

        // await using var transaction = await _playlistRepository.BeginTransactionOrExistingAsync();
        
        var playlist = await _playlistRepository.GetAsync(playlistId);

        if (!playlist.LogoFileId.HasValue)
        {
            throw new PlaylistHasNoLogoException(playlistId);
        }

        var logo = await _fileManager.DownloadAsync(playlist.LogoFileId.Value);

        log.ReturnsValue(logo);
        return logo;
    }

    /// <inheritdoc />
    public async Task UpdateLogoAsync(Guid userId, Guid playlistId, FileDal file)
    {
        using var log = new MethodLog(playlistId, file);

        // await using var transaction = await _playlistRepository.BeginTransactionOrExistingAsync();
        
        var playlist = await _playlistRepository.GetAsync(playlistId);

        if (playlist.CreatorId != userId)
        {
            throw new Exception($"User with id={userId} is not a creator of playlist with id={playlistId}");
        }

        // if (playlist.LogoFileId.HasValue)
        // {
        //     await _fileManager.DeleteAsync(playlist.LogoFileId.Value);
        // }

        await _fileManager.UploadSingleAsync(file);

        playlist.LogoFile = file;
        playlist.LogoFileId = file.Id;

        await _playlistRepository.UpdateAsync(playlist);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Guid playlistId, PlaylistDal newPlaylist)
    {
        using var log = new MethodLog(playlistId, newPlaylist);
        
        // await using var transaction = await _playlistRepository.BeginTransactionOrExistingAsync();
        
        var playlist = await _playlistRepository.GetAsync(playlistId);

        if (newPlaylist.Name != null)
        {
            playlist.Name = newPlaylist.Name;
        }

        if (newPlaylist.IsPrivate != playlist.IsPrivate)
        {
            playlist.IsPrivate = newPlaylist.IsPrivate;
        }

        await _playlistRepository.UpdateAsync(playlist);
    }
}