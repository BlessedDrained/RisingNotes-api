using MainLib.CustomException;
using RisingNotesLib.Constant;

namespace RisingNotesLib.Exceptions;

public class PlaylistDoesNotBelongToCurrentUser : BadRequestException
{
    public PlaylistDoesNotBelongToCurrentUser(Guid playlistId, Guid userId) : base($"Playlist with Id={playlistId} does not belong to user with Id={userId}", RisingNotesErrorConstants.PlaylistDoesNotBelongToCurrentUser)
    {
    }
}