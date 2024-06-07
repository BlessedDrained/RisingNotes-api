using MainLib.CustomException;
using RisingNotesLib.Constant;

namespace RisingNotesLib.Exceptions;

/// <summary>
/// В плейлисте нет такого трека
/// </summary>
public class PlaylistHasNoSuchTrackException : BadRequestException
{
    public PlaylistHasNoSuchTrackException(Guid playlistId, Guid songId) : base($"Playlist with id={playlistId} has not track with id={songId}", RisingNotesErrorConstants.PlaylistHasNoSuchTrack)
    {
    }
}