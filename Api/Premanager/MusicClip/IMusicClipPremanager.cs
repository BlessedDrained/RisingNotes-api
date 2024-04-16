using Api.Controllers.MusicClip.Dto.Request;
using Api.Controllers.MusicClip.Dto.Response;
using Dal.MusicClip;

namespace Api.Premanager.MusicClip;


public interface IMusicClipPremanager
{
    /// <summary>
    /// Загрузить клип
    /// </summary>
    Task<UploadClipResponse> UploadAsync(UploadClipRequest request, Guid authorId);

    /// <summary>
    /// Получить информацию о клипе
    /// </summary>
    Task<GetMusicClipInfoResponse> GetInfoAsync(Guid clipId);
}