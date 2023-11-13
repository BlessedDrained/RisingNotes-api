namespace Api.Controllers.Author.Dto.Response;

/// <summary>
/// Ответ на получение краткой информации об авторе
/// </summary>
public record GetAuthorShortInfoResponse
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Имя автора
    /// </summary>
    public string Name { get; init; }
}