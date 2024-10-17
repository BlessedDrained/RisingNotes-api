namespace Api.Controllers.Clip.Dto.Response;

/// <summary>
/// 
/// </summary>
public record GetClipIdBySongIdResponse
{
    /// <summary>
    /// Идентификатор песни
    /// </summary>
    public Guid ClipId { get; init; }
}