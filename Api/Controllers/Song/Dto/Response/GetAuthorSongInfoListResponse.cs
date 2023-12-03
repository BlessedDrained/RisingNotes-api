namespace Api.Controllers.Song.Dto.Response;

/// <summary>
/// Ответ на получение списка информации о песнях
/// </summary>
public record GetAuthorSongInfoListResponse
{
    /// <summary>
    /// Список информации о песнях
    /// </summary>
    public List<GetAuthorSongInfoResponse> SongInfoList { get; init; } = new();
}