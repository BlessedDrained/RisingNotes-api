using Api.Controllers.File.Dto.Request;
using Api.Controllers.Song.Dto.Response;
using Api.Controllers.Subscription.Dto.Response;

namespace Api.Premanager.User;

/// <summary>
/// Premanager для пользователя
/// </summary>
public interface IUserPremanager
{
    /// <summary>
    /// Получить список информации о плейлисте избранное
    /// </summary>
    Task<List<GetSongInfoResponse>> GetFavoriteSongInfoListAsync(Guid userId);

    /// <summary>
    /// Получить список подписок пользователя
    /// </summary>
    Task<GetSubscriptionListResponse> GetSubscriptionListAsync(Guid userId);

    /// <summary>
    /// Обновить логотип
    /// </summary>
    Task UpdateLogoAsync(Guid userId, UploadFileRequest request);
}