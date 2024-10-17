namespace Api.Controllers.Clip.Dto.Request;

/// <summary>
/// Запрос на получение идентификатора клипа по идентификатору песни
/// </summary>
public record GetClipIdBySongIdRequest
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid ClipId { get; init; }
}