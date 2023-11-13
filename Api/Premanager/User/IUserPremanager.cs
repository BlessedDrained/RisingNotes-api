using Api.Controllers.Song.Dto.Response;

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
}