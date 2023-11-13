namespace Api.Controllers.Author.Dto.Response;

/// <summary>
/// Ответ на получение списка авторов
/// </summary>
public record GetAuthorListResponse
{
    /// <summary>
    /// Список авторов
    /// </summary>
    public List<GetAuthorInfoResponse> AuthorList { get; init; } = new();
}