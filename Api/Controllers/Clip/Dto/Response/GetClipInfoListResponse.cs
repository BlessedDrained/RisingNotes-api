namespace Api.Controllers.Clip.Dto.Response;

/// <summary>
/// Ответ на получение списка клипов
/// </summary>
public record GetClipInfoListResponse
{
    /// <summary>
    /// Список клипов
    /// </summary>
    public List<GetClipInfoResponse> MusicClipList { get; init; } = new();
}