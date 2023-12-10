using Api.Controllers.File.Dto.Request;
using Api.Controllers.Playlist.Dto.Request;
using Api.Controllers.Playlist.Dto.Response;
using Api.Controllers.Song.Dto.Response;
using AutoMapper;
using Dal.File;
using Dal.Playlist;
using Dal.Playlist.Repository;
using Logic.Playlist;
using MainLib.Logging;
using Microsoft.EntityFrameworkCore.Query.Internal;
using RisingNotesLib.Models;

namespace Api.Premanager.Playlist;

/// <inheritdoc />
public class PlaylistPremanager : IPlaylistPremanager
{
    private readonly IPlaylistManager _playlistManager;
    private readonly IMapper _mapper;
    private readonly IPlaylistRepository _playlistRepository;

    ///
    public PlaylistPremanager(IPlaylistManager playlistManager, IMapper mapper, IPlaylistRepository playlistRepository)
    {
        _playlistManager = playlistManager;
        _mapper = mapper;
        _playlistRepository = playlistRepository;
    }

    /// <inheritdoc />
    public async Task<CreatePlaylistResponse> CreateAsync(CreatePlaylistRequest request, Guid userId)
    {
        // TODO: возможно, полезно будет, чтобы новые плейлисты назывались "Новый плейлист 1", "Новый плейлист 2" и т.д
        using var log = new MethodLog(request, userId);
        var playlist = _mapper.Map<PlaylistDal>(request);
        playlist.CreatorId = userId;

        var playlistId = await _playlistManager.CreateAsync(playlist);

        var response = new CreatePlaylistResponse()
        {
            Id = playlistId
        };
        log.ReturnsValue(response);

        return response;
    }


    /// <inheritdoc />
    public async Task<GetPlaylistInfoResponse> GetInfoAsync(Guid playlistId)
    {
        using var log = new MethodLog(playlistId);
        var playlist = await _playlistManager.GetInfoAsync(playlistId);

        var response = _mapper.Map<GetPlaylistInfoResponse>(playlist);

        log.ReturnsValue(response);
        return response;
    }

    /// <inheritdoc />
    public async Task<GetUserPlaylistInfoListResponse> GetUserPlaylistInfoListAsync(Guid userId)
    {
        using var log = new MethodLog(userId);

        var playlistInfoList = await _playlistManager.GetUserPlaylistInfoList(userId);

        var responseList = _mapper.Map<List<GetPlaylistInfoResponse>>(playlistInfoList);
        var response = new GetUserPlaylistInfoListResponse()
        {
            PlaylistInfoList = responseList
        };

        log.ReturnsValue(response);
        return response;
    }

    /// <inheritdoc />
    public async Task<GetPlaylistSongListResponse> GetSongListAsync(Guid playlistId)
    {
        using var log = new MethodLog(playlistId);
        var songList = await _playlistManager.GetSongListAsync(playlistId);

        var songInfoResponseList = _mapper.Map<List<GetWithAuthorSongInfoResponse>>(songList);

        var response = new GetPlaylistSongListResponse()
        {
            SongList = songInfoResponseList
        };

        log.ReturnsValue(response);
        return response;
    }

    /// <inheritdoc />
    public async Task UpdateLogoAsync(Guid userId, Guid playlistId, UploadFileRequest request)
    {
        using var log = new MethodLog(userId, playlistId, request);

        var file = _mapper.Map<FileDal>(request);
        await _playlistManager.UpdateLogoAsync(userId, playlistId, file);
    }

    /// <inheritdoc />
    public async Task<GetPlaylistListResponse> GetListAsync(Guid? userId, GetPlaylistListRequest request)
    {
        using var log = new MethodLog(request);

        var filter = _mapper.Map<GetPlaylistListFilterModel>(request);
        var playlistList = await _playlistRepository.GetListAsync(userId, filter);

        var responseList = _mapper.Map<List<GetPlaylistInfoResponse>>(playlistList);

        var response = new GetPlaylistListResponse()
        {
            PlaylistList = responseList
        };
        return response;
    }

    /// <inheritdoc />
    public Task UpdateAsync(Guid playlistId, UpdatePlaylistRequest request)
    {
        using var log = new MethodLog(request);

        var newPlaylist = _mapper.Map<PlaylistDal>(request);
        
        return _playlistManager.UpdateAsync(playlistId, newPlaylist);
    }
}