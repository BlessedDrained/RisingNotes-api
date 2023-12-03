using Api.Controllers.File.Dto.Request;
using Api.Controllers.Song.Dto.Request;
using Api.Controllers.Song.Dto.Response;
using RisingNotesLib.Models;

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
    Task<GetWithAuthorSongInfoResponse> GetSongInfoAsync(Guid songId);

    /// <summary>
    /// Получить список информации о треках автора
    /// </summary>
    Task<GetAuthorSongInfoListResponse> GetAuthorSongInfoListAsync(Guid authorId);

    /// <summary>
    /// Получить список информации об избранных треках
    /// </summary>
    Task<GetFavoriteSongInfoListResponse> GetFavoriteSongInfoList(Guid userId);

    /// <summary>
    /// Получить список треков по фильтрам
    /// </summary>
    Task<GetSongListResponse> GetSongListAsync(GetSongListFilterModel filter);

    /// <summary>
    /// Обновить логотип
    /// </summary>
    Task UpdateLogoAsync(Guid authorId, Guid songId, UploadFileRequest request);

    /// <summary>
    /// Получить количество прослушиваний песни
    /// </summary>
    Task<GetAuditionCountResponse> GetAuditionCountAsync(Guid songId);
}