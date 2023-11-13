using RisingNotesLib.Models;

namespace Api.Controllers.SongPublish.Dto.Request;

/// <summary>
/// Запрос на получение списка заявок
/// </summary>
public record GetPublishRequestListRequest : GetPublishRequestListFilterModel
{
}