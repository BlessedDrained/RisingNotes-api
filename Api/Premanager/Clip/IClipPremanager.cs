using Api.Controllers.Clip.Dto.Request;
using Api.Controllers.Clip.Dto.Response;

namespace Api.Premanager.Clip;


public interface IClipPremanager
{
    /// <summary>
    /// Загрузить клип
    /// </summary>
    Task<UploadClipResponse> UploadAsync(UploadClipRequest request, Guid authorId);

    /// <summary>
    /// Получить информацию о клипе
    /// </summary>
    Task<GetClipInfoResponse> GetInfoAsync(Guid clipId);

    /// <summary>
    /// Получить список клипов по вайлдкарду названия
    /// </summary>
    Task<GetClipInfoListResponse> GetListAsync(string nameWildcard);

    /// <summary>
    /// Получить список клипов автора
    /// </summary>
    Task<GetClipInfoListResponse> GetAuthorClipListAsync(Guid authorId);
}