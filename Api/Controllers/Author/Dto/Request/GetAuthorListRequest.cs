namespace Api.Controllers.Author.Dto.Request;

/// <summary>
/// Запрос на получение списка авторов
/// </summary>
public record GetAuthorListRequest
{
    /// <summary>
    /// Имя/часть имени
    /// </summary>
    public string NameWildcard { get; init; }
}