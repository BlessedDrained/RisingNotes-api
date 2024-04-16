using Api.Controllers.MusicClip.Dto.Request;
using Api.Controllers.MusicClip.Dto.Response;
using AutoMapper;
using Dal.File;
using Dal.MusicClip;
using Logic.MusicClip;
using MainLib.Logging;

namespace Api.Premanager.MusicClip;

/// <inheritdoc />
public class MusicClipPremanager : IMusicClipPremanager
{
    private readonly IMusicClipManager _musicClipManager;
    private readonly IMapper _mapper;

    public MusicClipPremanager(IMusicClipManager musicClipManager, IMapper mapper)
    {
        _musicClipManager = musicClipManager;
        _mapper = mapper;
    }
    
    /// <inheritdoc />
    public async Task<UploadClipResponse> UploadAsync(UploadClipRequest request, Guid authorId)
    {
        using var log = new MethodLog(request, authorId);
        
        var clip = _mapper.Map<MusicClipDal>(request);
        var clipFile = _mapper.Map<FileDal>(request.ClipFile);
        var previewFile = _mapper.Map<FileDal>(request.PreviewFile);
        clip.UploaderId = authorId;
        
        var id = await _musicClipManager.UploadAsync(clip, clipFile, previewFile);

        var response = new UploadClipResponse()
        {
            Id = id
        };
        
        log.ReturnsValue(response);
        return response;
    }

    /// <inheritdoc />
    public async Task<GetMusicClipInfoResponse> GetInfoAsync(Guid clipId)
    {
        using var log = new MethodLog(clipId);

        var clip = await _musicClipManager.GetAsync(clipId);

        var response = _mapper.Map<GetMusicClipInfoResponse>(clip);
        log.ReturnsValue(response);

        return response;
    }
}