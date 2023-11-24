namespace Api.Controllers.Song.Dto.Response;

/// <summary>
/// Модель ответа на получение списка треков по фильтрам
/// </summary>
public record GetSongListResponse
{
    /// <summary>
    /// Список песен
    /// </summary>
    public List<GetSongInfoResponse> SongList { get; init; } = new();
}