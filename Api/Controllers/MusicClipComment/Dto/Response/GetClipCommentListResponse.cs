namespace Api.Controllers.MusicClipComment.Dto.Response;

/// <summary>
/// Ответ на получение списка комментариев к клипу
/// </summary>
public record GetClipCommentListResponse
{
    /// <summary>
    /// Список комментариев
    /// </summary>
    public List<GetClipCommentResponse> CommentList { get; init; } = new();
}