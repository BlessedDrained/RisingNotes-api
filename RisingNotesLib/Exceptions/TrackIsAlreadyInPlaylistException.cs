using MainLib.CustomException;
using RisingNotesLib.Constant;

namespace RisingNotesLib.Exceptions;

public class TrackIsAlreadyInPlaylistException : BadRequestException
{
    public TrackIsAlreadyInPlaylistException(Guid songId, Guid playlistId) 
        : base($"Song with Id={songId} is already in playlist with Id={playlistId}", RisingNotesErrorConstants.TrackIsAlreadyInPlaylistException)
    {
    }
}