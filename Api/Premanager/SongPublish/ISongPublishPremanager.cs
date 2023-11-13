using Api.Controllers.SongPublish.Dto.Request;
using Api.Controllers.SongPublish.Dto.Response;

namespace Api.Premanager.SongPublish;

/// <summary>
/// Premanager для работы с зяавками на публикацию песни
/// </summary>
public interface ISongPublishPremanager
{
    /// <summary>
    /// Создать заявку
    /// </summary>
    Task<CreateSongPublishRequestResponse> CreateAsync(Guid userId, CreateSongPublishRequestRequest request);

    /// <summary>
    /// Ответить на заявку от лица пользователя
    /// </summary>
    Task ReplyRequestAsUserAsync(Guid requestId, ReplyToRequestAsUserRequest request);

    /// <summary>
    /// Получить список с краткой информацией о заявках
    /// </summary>
    Task<GetPublishRequestShortInfoListResponse> GetListAsync(GetPublishRequestListRequest request, bool isAdmin);
}