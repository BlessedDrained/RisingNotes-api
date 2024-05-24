using Api.Controllers.Author.Dto.Request;
using Api.Controllers.Author.Dto.Response;
using Api.Controllers.Song.Dto.Response;
using Api.Controllers.Subscription.Dto.Response;

namespace Api.Premanager.Author;

/// <summary>
/// premanager для авторов/>
/// </summary>
public interface IAuthorPremanager
{
    /// <summary>
    /// Сделать автором
    /// </summary>
    Task<Guid> MakeAuthorAsync(MakeAuthorRequest request);

    /// <summary>
    /// Получить краткую информацию об авторе
    /// </summary>
    /// <remarks>Включает только id и имя автора</remarks>
    Task<GetAuthorShortInfoResponse> GetShortInfoAsync(string authorName);

    /// <summary>
    /// Получить информацию об авторе
    /// </summary>
    /// <remarks>Не включает информацию о треках</remarks>
    Task<GetAuthorInfoResponse> GetInfoAsync(string authorName);

    /// <summary>
    /// Получить информацию об авторе
    /// </summary>
    /// <remarks>Не включает информацию о треках</remarks>
    Task<GetAuthorInfoResponse> GetInfoAsync(Guid authorId);

    /// <summary>
    /// Получить список авторов
    /// </summary>
    Task<GetAuthorListResponse> GetListAsync(GetAuthorListRequest request);

    /// <summary>
    /// Получить список информации о треках автора
    /// </summary>
    Task<GetAuthorSongInfoListResponse> GetAuthorSongInfoListAsync(Guid authorId);

    /// <summary>
    /// Получить количество подписчиков автора
    /// </summary>
    Task<GetSubscriberCountResponse> GetSubscriberCountAsync(Guid authorId);

    /// <summary>
    /// Обновить информацию об авторе
    /// </summary>
    Task UpdateAsync(Guid authorId, UpdateAuthorRequest request);

    /// <summary>
    /// Получить общее количество прослушиваний автора
    /// </summary>
    Task<GetAuthorTotalAuditionCountResponse> GetAuthorTotalAuditionCountAsync(Guid authorId);
}