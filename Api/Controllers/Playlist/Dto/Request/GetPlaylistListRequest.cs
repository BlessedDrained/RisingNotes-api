using RisingNotesLib.Models;

namespace Api.Controllers.Playlist.Dto.Request;

/// <summary>
/// Запрос на получение списка плейлистов по фильтрам
/// </summary>
public record GetPlaylistListRequest : GetPlaylistListFilterModel
{
    
}