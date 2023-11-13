namespace Api.Controllers.SongComment.Request;

/// <summary>
/// Запрос на добавление комментария
/// </summary>
public record AddCommentRequest
{
    /// <summary>
    /// Текст
    /// </summary>
    public string Text { get; init; }
}