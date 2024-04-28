namespace Api.Controllers.ShortVideoComment.Dto.Response;

/// <summary>
/// Ответ на получение списка комментариев к короткому видео
/// </summary>
public record GetShortVideoCommentListResponse
{
    /// <summary>
    /// Список комментариев
    /// </summary>
    public List<GetShortVideoCommentResponse> CommentList { get; init; } = new();
}