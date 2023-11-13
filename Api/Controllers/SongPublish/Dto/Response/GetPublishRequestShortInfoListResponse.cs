namespace Api.Controllers.SongPublish.Dto.Response;

/// <summary>
/// Ответ на получение списка краткой инфы о заявках
/// </summary>
public record GetPublishRequestShortInfoListResponse
{
    /// <summary>
    /// Список краткой информации о заявках
    /// </summary>
    public List<GetPublishRequestShortInfoResponse> PublishRequestShortInfoList { get; init; } = new();
}