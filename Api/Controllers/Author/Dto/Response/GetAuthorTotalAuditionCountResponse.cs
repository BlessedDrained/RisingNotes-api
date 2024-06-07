namespace Api.Controllers.Author.Dto.Response;

/// <summary>
/// Ответ на получение общего количества прослушиваний
/// </summary>
public record GetAuthorTotalAuditionCountResponse
{
    /// <summary>
    /// Общее количество прослушиваний
    /// </summary>
    public int AuditionCount { get; init; }
}