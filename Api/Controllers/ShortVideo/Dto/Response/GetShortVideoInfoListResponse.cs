namespace Api.Controllers.ShortVideo.Dto.Response;

/// <summary>
/// Ответ на получение информации о списке коротких видео
/// </summary>
public record GetShortVideoInfoListResponse
{
    /// <summary>
    /// Информация о клипах
    /// </summary>
    public List<GetShortVideoInfoResponse> ShortVideoList { get; init; } = new();
}