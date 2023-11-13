namespace Api.Controllers.SongComment.Request;

/// <summary>
/// Запрос на редактирование комментария
/// </summary>
public record EditCommentRequest
{
    /// <summary>
    /// Новый текст
    /// </summary>
    public string NewText { get; init; }
}