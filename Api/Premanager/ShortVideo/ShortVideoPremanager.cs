using Api.Controllers.ShortVideo.Dto.Request;
using Api.Controllers.ShortVideo.Dto.Response;
using AutoMapper;
using Dal.File;
using Dal.ShortVideo;
using Dal.ShortVideo.Repository;
using Logic.ShortVideo;
using MainLib.Logging;

namespace Api.Premanager.ShortVideo;

/// <inheritdoc />
public class ShortVideoPremanager : IShortVideoPremanager
{
    private readonly IShortVideoManager _shortVideoManager;
    private readonly IMapper _mapper;
    private readonly IShortVideoRepository _shortVideoRepository;

    public ShortVideoPremanager(IShortVideoManager shortVideoManager, IMapper mapper, IShortVideoRepository shortVideoRepository)
    {
        _shortVideoManager = shortVideoManager;
        _mapper = mapper;
        _shortVideoRepository = shortVideoRepository;
    }
    
    /// <inheritdoc />
    public async Task<UploadShortVideoResponse> UploadAsync(UploadShortVideoRequest request, Guid authorId)
    {
        using var log = new MethodLog(request, authorId);
        
        var shortVideo = _mapper.Map<ShortVideoDal>(request);
        var videoFile = _mapper.Map<FileDal>(request.ClipFile);
        var previewFile = _mapper.Map<FileDal>(request.PreviewFile);
        shortVideo.UploaderId = authorId;
        
        var id = await _shortVideoManager.UploadAsync(shortVideo, videoFile, previewFile);

        var response = new UploadShortVideoResponse()
        {
            Id = id
        };
        
        log.ReturnsValue(response);
        return response;
    }

    /// <inheritdoc />
    public async Task<GetShortVideoInfoResponse> GetInfoAsync(Guid clipId)
    {
        using var log = new MethodLog(clipId);

        var clip = await _shortVideoManager.GetAsync(clipId);

        var response = _mapper.Map<GetShortVideoInfoResponse>(clip);
        log.ReturnsValue(response);

        return response;
    }
    
    /// <inheritdoc />
    public async Task<GetShortVideoInfoListResponse> GetListAsync(string nameWildcard)
    {
        using var log = new MethodLog(nameWildcard);

        var lowerWildcard = nameWildcard.ToLower();
        var clipList = await _shortVideoRepository
            .GetListAsync(x => x.Title.ToLower().Contains(lowerWildcard));

        var responseList = _mapper.Map<List<GetShortVideoInfoResponse>>(clipList);
        var response = new GetShortVideoInfoListResponse()
        {
            ShortVideoList = responseList
        };

        log.ReturnsValue(response);
        
        return response;
    }

    /// <inheritdoc />
    public async Task<GetShortVideoInfoListResponse> GetAuthorClipListAsync(Guid authorId)
    {
        using var log = new MethodLog(authorId);

        var clipList = await _shortVideoRepository.GetListAsync(x => x.UploaderId == authorId);

        var responseList = _mapper.Map<List<GetShortVideoInfoResponse>>(clipList);

        var response = new GetShortVideoInfoListResponse()
        {
            ShortVideoList = responseList
        };
        log.ReturnsValue(response);

        return response;
    }
}