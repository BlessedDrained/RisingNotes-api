using Api.Controllers.MusicClip.Dto.Request;
using Api.Controllers.MusicClip.Dto.Response;
using Api.Controllers.ShortVideo.Dto.Request;
using Api.Controllers.ShortVideo.Dto.Response;
using AutoMapper;
using Dal.File;
using Dal.MusicClip;
using Dal.ShortVideo;
using Logic.ShortVideo;
using MainLib.Logging;

namespace Api.Premanager.ShortVideo;

/// <inheritdoc />
public class ShortVideoPremanager : IShortVideoPremanager
{
    private readonly IShortVideoManager _shortVideoManager;
    private readonly IMapper _mapper;

    public ShortVideoPremanager(IShortVideoManager shortVideoManager, IMapper mapper)
    {
        _shortVideoManager = shortVideoManager;
        _mapper = mapper;
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
}