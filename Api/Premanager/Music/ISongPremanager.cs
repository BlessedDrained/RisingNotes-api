using Api.Controllers.Song.Dto.Request;
using Api.Controllers.Song.Dto.Response;

namespace Api.Premanager.Music;

/// 
public interface ISongPremanager
{
    /// <summary>
    /// Создать трек
    /// </summary>
    Task<CreateSongResponse> CreateAsync(UploadSongRequest request, Guid authorId);

    /// <summary>
    /// Получить информацию о треке
    /// </summary>
    Task<GetSongInfoResponse> GetSongInfoAsync(Guid songId);

    /// <summary>
    /// Получить список информации о треках автора
    /// </summary>
    Task<GetAuthorSongInfoListResponse> GetAuthorSongInfoListAsync(Guid authorId);

    /// <summary>
    /// Получить список информации об избранных треках
    /// </summary>
    Task<GetFavoriteSongInfoListResponse> GetFavoriteSongInfoList(Guid userId);
}