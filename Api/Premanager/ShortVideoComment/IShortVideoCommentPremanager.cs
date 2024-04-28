using Api.Controllers.ShortVideoComment.Dto.Response;
using Dal.ShortVideoComment;

namespace Api.Premanager.ShortVideoComment;

/// <summary>
/// premanager для <see cref="ShortVideoCommentDal"/>
/// </summary>
public interface IShortVideoCommentPremanager
{
    /// <summary>
    /// Получить список комментариев к песне
    /// </summary>
    Task<GetShortVideoCommentListResponse> GetShortVideoCommentListAsync(Guid musicClipId);
}