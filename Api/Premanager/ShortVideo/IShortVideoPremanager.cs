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
    
    /// <summary>
    /// Получить список коротких видео по вайлдкарду названия
    /// </summary>
    Task<GetShortVideoInfoListResponse> GetListAsync(string nameWildcard);
    
    /// <summary>
    /// Получить список коротких видео автора
    /// </summary>
    Task<GetShortVideoInfoListResponse> GetAuthorClipListAsync(Guid authorId);
}