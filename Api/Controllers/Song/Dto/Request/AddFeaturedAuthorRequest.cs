namespace Api.Controllers.Song.Dto.Request;

/// <summary>
/// Запрос на добавление автора в песню
/// </summary>
public record AddFeaturedAuthorRequest
{
    /// <summary>
    /// Идентификатор автора
    /// </summary>
    public Guid? AuthorId { get; init; }
    
    /// <summary>
    /// Псевдоним автора
    /// </summary>
    public string AuthorName { get; init; }
}