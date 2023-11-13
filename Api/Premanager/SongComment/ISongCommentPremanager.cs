using Api.Controllers.SongComment.Response;

namespace Api.Premanager.SongComment;

/// <summary>
/// premanager для <see cref="SongCommentDal"/>
/// </summary>
public interface ISongCommentPremanager
{
    /// <summary>
    /// Получить список комментариев к песне
    /// </summary>
    Task<GetSongCommentListResponse> GetSongCommentListAsync(Guid songId);
}