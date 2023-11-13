using RisingNotesLib.Enums;

namespace Api.Controllers.SongPublish.Dto.Request;

/// <summary>
/// Запрос на ревью
/// </summary>
public record ReplyToRequestAsAdminRequest
{
    /// <summary>
    /// Новый статус
    /// </summary>
    public PublishRequestStatus Status { get; init; }
    
    /// <summary>
    /// Комментарий
    /// </summary>
    public string Comment { get; init; }
}