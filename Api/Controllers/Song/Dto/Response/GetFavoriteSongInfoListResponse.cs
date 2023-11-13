namespace Api.Controllers.Song.Dto.Response;

/// <summary>
/// Получить список избранных треков
/// </summary>
public record GetFavoriteSongInfoListResponse
{
    /// <summary>
    /// Список информации об избранных треках
    /// </summary>
    public List<GetWithAuthorSongInfoResponse> SongInfoList { get; init; } = new();
}