using Api.Controllers.ExcludedTrack.Dto;
using Api.Controllers.File.Dto.Request;
using Api.Controllers.Song.Dto.Response;
using Api.Controllers.Subscription.Dto.Response;
using Api.Controllers.User.Dto.Response;

namespace Api.Premanager.User;

/// <summary>
/// Premanager для пользователя
/// </summary>
public interface IUserPremanager
{
    /// <summary>
    /// Получить список информации о плейлисте избранное
    /// </summary>
    Task<List<GetWithAuthorSongInfoResponse>> GetFavoriteSongInfoListAsync(Guid userId);

    /// <summary>
    /// Получить список подписок пользователя
    /// </summary>
    Task<GetSubscriptionListResponse> GetSubscriptionListAsync(Guid userId);

    /// <summary>
    /// Обновить логотип
    /// </summary>
    Task UpdateLogoAsync(Guid userId, UploadFileRequest request);

    /// <summary>
    /// Получить список исключенных треков
    /// </summary>
    Task<GetExcludedTrackListResponse> GetExcludedTrackListAsync(Guid userId);

    /// <summary>
    /// Получить информацию о пользователе
    /// </summary>
    Task<GetUserResponse> GetAsync(Guid id);
}