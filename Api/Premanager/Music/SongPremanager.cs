using Api.Controllers.Song.Dto.Request;
using Api.Controllers.Song.Dto.Response;
using AutoMapper;
using Dal.File;
using Dal.Song;
using Logic.Song;
using MainLib.Extensions;
using MainLib.Logging;
using MainLib.TagLib;
using File = TagLib.File;

namespace Api.Premanager.Music;

/// <inheritdoc />
public class SongPremanager : ISongPremanager
{
    private readonly ISongManager _songManager;
    private readonly IMapper _mapper;

    ///
    public SongPremanager(
        ISongManager songManager,
        IMapper mapper)
    {
        _songManager = songManager;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<CreateSongResponse> CreateAsync(UploadSongRequest request, Guid authorId)
    {
        using var log = new MethodLog(request, authorId);

        var song = _mapper.Map<SongDal>(request);
        var songFile = _mapper.Map<FileDal>(request.SongFile);
        var logoFile = _mapper.Map<FileDal>(request.SongLogo);
        song.AuthorId = authorId;

        var id = await _songManager.CreateAsync(song, songFile, logoFile);

        var response = new CreateSongResponse()
        {
            Id = id
        };
        return response;
    }

    /// <inheritdoc />
    public async Task<GetSongInfoResponse> GetSongInfoAsync(Guid songId)
    {
        using var log = new MethodLog(songId);

        var songInfo = await _songManager.GetSongInfoAsync(songId);

        var response = _mapper.Map<GetSongInfoResponse>(songInfo);

        log.ReturnsValue(response);
        return response;
    }

    /// <inheritdoc />
    public async Task<GetAuthorSongInfoListResponse> GetAuthorSongInfoListAsync(Guid authorId)
    {
        using var log = new MethodLog(authorId);

        var authorSongInfoList = await _songManager.GetAuthorSongInfoListAsync(authorId);

        var responseList = _mapper.Map<List<GetSongInfoResponse>>(authorSongInfoList);
        var response = new GetAuthorSongInfoListResponse()
        {
            SongInfoList = responseList
        };

        log.ReturnsValue(response);
        return response;
    }

    /// <inheritdoc />
    public async Task<GetFavoriteSongInfoListResponse> GetFavoriteSongInfoList(Guid userId)
    {
        using var log = new MethodLog(userId);

        var favoriteSongInfoList = await _songManager.GetFavoriteSongInfoListAsync(userId);

        var responseList = _mapper.Map<List<GetWithAuthorSongInfoResponse>>(favoriteSongInfoList);
        var response = new GetFavoriteSongInfoListResponse()
        {
            SongInfoList = responseList
        };

        log.ReturnsValue(response);
        return response;
    }
}