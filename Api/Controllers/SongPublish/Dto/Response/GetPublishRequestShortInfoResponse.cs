using RisingNotesLib.Enums;

namespace Api.Controllers.SongPublish.Dto.Response;

/// <summary>
/// Ответ на получение списка краткой информации о заявках
/// </summary>
public record GetPublishRequestShortInfoResponse
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; init; }
    
    /// <summary>
    /// Идентификатор автора
    /// </summary>
    public Guid AuthorId { get; init; }
    
    /// <summary>
    /// Имя автора
    /// </summary>
    public string AuthorName { get; init; }
    
    /// <summary>
    /// Статус
    /// </summary>
    public PublishRequestStatus Status { get; init; }
}