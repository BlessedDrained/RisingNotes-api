namespace Api.Controllers.SongComment.Response;

/// <summary>
/// Ответ на получение комментария
/// </summary>
public record GetSongCommentResponse
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