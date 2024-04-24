using Api.Controllers.MusicClip.Dto.Request;
using Api.Controllers.MusicClip.Dto.Response;
using Api.Controllers.ShortVideo.Dto.Request;
using Api.Controllers.ShortVideo.Dto.Response;

namespace Api.Premanager.ShortVideo;


public interface IShortVideoPremanager
{
    /// <summary>
    /// Загрузить клип
    /// </summary>
    Task<UploadShortVideoResponse> UploadAsync(UploadShortVideoRequest request, Guid authorId);

    /// <summary>
    /// Получить информацию о клипе
    /// </summary>
    Task<GetShortVideoInfoResponse> GetInfoAsync(Guid clipId);
}