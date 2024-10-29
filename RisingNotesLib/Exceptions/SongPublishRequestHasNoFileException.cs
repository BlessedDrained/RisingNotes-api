using MainLib.CustomException;
using RisingNotesLib.Constant;

namespace RisingNotesLib.Exceptions;

/// <inheritdoc cref="RisingNotesErrorConstants.SongPublishRequestHasNoLogo"/>
public class SongPublishRequestHasNoFileException : BadRequestException
{
    public SongPublishRequestHasNoFileException(string message) 
        : base(message, RisingNotesErrorConstants.SongPublishRequestHasNoFile)
    {
    }

    public SongPublishRequestHasNoFileException(Guid songPublishRequestId) 
        : base($"Song publish request with id={songPublishRequestId} has no song", RisingNotesErrorConstants.SongPublishRequestHasNoFile)
    {
    }
}