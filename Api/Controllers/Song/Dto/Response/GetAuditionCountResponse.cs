namespace Api.Controllers.Song.Dto.Response;

/// <summary>
/// Ответ на получение количества прослушиваний трека 
/// </summary>
public record GetAuditionCountResponse
{
    /// <summary>
    /// Количество прослушиваний
    /// </summary>
    public int AuditionCount { get; init; }
}