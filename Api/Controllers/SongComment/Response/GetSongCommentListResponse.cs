namespace Api.Controllers.SongComment.Response;

/// <summary>
/// Ответ на получение списка комментариев к песне
/// </summary>
public record GetSongCommentListResponse
{
    /// <summary>
    /// Список комментариев
    /// </summary>
    public List<GetSongCommentResponse> CommentList { get; init; } = new();
}