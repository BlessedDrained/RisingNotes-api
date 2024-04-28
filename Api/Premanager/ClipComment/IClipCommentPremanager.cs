using Api.Controllers.ClipComment.Dto.Response;
using Dal.MusicClip;

namespace Api.Premanager.ClipComment;

/// <summary>
/// Premanager для <see cref="ClipDal"/>
/// </summary>
public interface IClipCommentPremanager
{
    /// <summary>
    /// Получить список комментариев к песне
    /// </summary>
    Task<GetClipCommentListResponse> GetClipCommentListAsync(Guid musicClipId);
}