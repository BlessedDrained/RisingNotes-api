﻿using Api.Controllers.File.Dto.Request;
using Api.Controllers.Song.Dto.Request;
using Api.Controllers.Song.Dto.Response;
using AutoMapper;
using Dal.File;
using Dal.Song;
using Dal.Song.Repository;
using Logic.Song;
using MainLib.Logging;
using RisingNotesLib.Models;

namespace Api.Premanager.Music;

/// <inheritdoc />
public class SongPremanager : ISongPremanager
{
    private readonly ISongManager _songManager;
    private readonly IMapper _mapper;
    private readonly ISongRepository _songRepository;

    ///
    public SongPremanager(
        ISongManager songManager,
        IMapper mapper,
        ISongRepository songRepository)
    {
        _songManager = songManager;
        _mapper = mapper;
        _songRepository = songRepository;
    }

    /// <inheritdoc />
    public async Task<UploadSongResponse> CreateAsync(UploadSongRequest request, Guid authorId)
    {
        using var log = new MethodLog(request, authorId);

        var song = _mapper.Map<SongDal>(request);
        song.AuthorId = authorId;

        var id = await _songManager.CreateAsync(song);

        var response = new UploadSongResponse()
        {
            Id = id
        };
        return response;
    }

    /// <inheritdoc />
    public async Task UploadLogoAsync(UploadSongLogoRequest request, Guid authorId)
    {
        using var log = new MethodLog(request);

        await _songManager.UploadLogoAsync(request.SongId, authorId, request.LogoFile);
    }

    /// <inheritdoc />
    public async Task<GetWithAuthorSongInfoResponse> GetSongInfoAsync(Guid songId)
    {
        using var log = new MethodLog(songId);

        var songInfo = await _songManager.GetSongInfoAsync(songId);

        var response = _mapper.Map<GetWithAuthorSongInfoResponse>(songInfo);

        log.ReturnsValue(response);
        return response;
    }

    /// <inheritdoc />
    public async Task<GetAuthorSongInfoListResponse> GetAuthorSongInfoListAsync(Guid authorId)
    {
        using var log = new MethodLog(authorId);

        var authorSongInfoList = await _songManager.GetAuthorSongInfoListAsync(authorId);

        var responseList = _mapper.Map<List<GetAuthorSongInfoResponse>>(authorSongInfoList);
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

    /// <inheritdoc />
    public async Task<GetSongListResponse> GetSongListAsync(GetSongListFilterModel filter)
    {
        using var log = new MethodLog(filter);

        var songList = await _songRepository.GetListAsync(filter);

        var responseList = _mapper.Map<List<GetWithAuthorSongInfoResponse>>(songList);
        var response = new GetSongListResponse()
        {
            SongList = responseList
        };

        log.ReturnsValue(response);
        return response;
    }

    /// <inheritdoc />
    public async Task UpdateLogoAsync(Guid authorId, Guid songId, UploadFileRequest request)
    {
        using var log = new MethodLog(authorId, songId, request);

        var file = _mapper.Map<FileDal>(request);
        await _songManager.UpdateLogoAsync(authorId, songId, file);
    }

    /// <inheritdoc />
    public async Task<GetAuditionCountResponse> GetAuditionCountAsync(Guid songId)
    {
        using var log = new MethodLog(songId);

        var auditionCount = await _songManager.GetAuditionCountAsync(songId);

        var response = new GetAuditionCountResponse()
        {
            AuditionCount = auditionCount
        };
        return response;
    }
}