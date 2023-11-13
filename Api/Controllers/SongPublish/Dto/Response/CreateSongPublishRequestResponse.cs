namespace Api.Controllers.SongPublish.Dto.Response;

/// <summary>
/// 
/// </summary>
public record CreateSongPublishRequestResponse
{
    /// <summary>
    /// Идентификатор заявки
    /// </summary>
    public Guid Id { get; init; }
}