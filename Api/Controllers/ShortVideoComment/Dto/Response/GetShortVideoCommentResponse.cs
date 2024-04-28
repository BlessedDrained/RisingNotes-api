namespace Api.Controllers.ShortVideoComment.Dto.Response;

/// <summary>
/// Ответ на получение комментария к короткому видео
/// </summary>
public record GetShortVideoCommentResponse
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Идентификатор автора
    /// </summary>
    public Guid AuthorId { get; init; }

    /// <summary>
    /// Отображаемое имя автора
    /// </summary>
    public string AuthorDisplayedName { get; init; }

    /// <summary>
    /// Является ли автор комментария исполнителем
    /// </summary>
    public bool IsSongAuthor { get; init; }

    /// <summary>
    /// Текст
    /// </summary>
    public string Text { get; init; }
}
