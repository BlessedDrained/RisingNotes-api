namespace Api.Controllers.ExcludedTrack.Dto;

/// <summary>
/// Ответ на получение списка исключенных треков
/// </summary>
public record GetExcludedTrackListResponse
{
    /// <summary>
    /// Список исключенных треков
    /// </summary>
    public List<GetExcludedTrackInfoResponse> ExcludedTrackList { get; init; } = new();
}