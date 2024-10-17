using Api.Controllers.Clip.Dto.Request;
using Api.Controllers.Clip.Dto.Response;
using AutoMapper;
using Dal.File;
using Dal.MusicClip;
using Dal.MusicClip.Repository;
using Logic.MusicClip;
using MainLib.Logging;

namespace Api.Premanager.Clip;

/// <inheritdoc />
public class ClipPremanager : IClipPremanager
{
    private readonly IClipManager _clipManager;
    private readonly IMapper _mapper;
    private readonly IClipRepository _clipRepository;

    public ClipPremanager(IClipManager clipManager, IMapper mapper, IClipRepository clipRepository)
    {
        _clipManager = clipManager;
        _mapper = mapper;
        _clipRepository = clipRepository;
    }
    
    /// <inheritdoc />
    public async Task<UploadClipResponse> UploadAsync(UploadClipRequest request, Guid authorId)
    {
        using var log = new MethodLog(request, authorId);
        
        var clip = _mapper.Map<ClipDal>(request);
        var clipFile = _mapper.Map<FileDal>(request.ClipFile);
        var previewFile = _mapper.Map<FileDal>(request.PreviewFile);
        clip.UploaderId = authorId;
        
        var id = await _clipManager.UploadAsync(clip, clipFile, previewFile);

        var response = new UploadClipResponse()
        {
            Id = id
        };
        
        log.ReturnsValue(response);
        return response;
    }

    /// <inheritdoc />
    public async Task<GetClipInfoResponse> GetInfoAsync(Guid clipId)
    {
        using var log = new MethodLog(clipId);

        var clip = await _clipManager.GetAsync(clipId);

        var response = _mapper.Map<GetClipInfoResponse>(clip);
        log.ReturnsValue(response);

        return response;
    }

    /// <inheritdoc />
    public async Task<GetClipInfoListResponse> GetListAsync(string nameWildcard)
    {
        using var log = new MethodLog(nameWildcard);

        var lowerWildcard = nameWildcard.ToLower();
        var clipList = await _clipRepository
            .GetListAsync(x => x.Title.ToLower().Contains(lowerWildcard));

        var responseList = _mapper.Map<List<GetClipInfoResponse>>(clipList);
        var response = new GetClipInfoListResponse()
        {
            MusicClipList = responseList
        };

        log.ReturnsValue(response);
        
        return response;
    }

    /// <inheritdoc />
    public async Task<GetClipInfoListResponse> GetAuthorClipListAsync(Guid authorId)
    {
        using var log = new MethodLog(authorId);

        var clipList = await _clipRepository.GetListAsync(x => x.UploaderId == authorId);

        var responseList = _mapper.Map<List<GetClipInfoResponse>>(clipList);

        var response = new GetClipInfoListResponse()
        {
            MusicClipList = responseList
        };
        log.ReturnsValue(response);

        return response;
    }


    /// <inheritdoc />
    public async Task<GetClipIdBySongIdRequest> GetClipIdBySongIdAsync(Guid songId)
    {
        using var log = new MethodLog(songId);

        var clipId = await _clipRepository.FirstByFieldAsync(x => x.SongId == songId);

        return new GetClipIdBySongIdRequest()
        {
            ClipId = clipId.Id
        };
    }
}