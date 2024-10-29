using MainLib.CustomException;
using RisingNotesLib.Constant;

namespace RisingNotesLib.Exceptions;

/// <inheritdoc cref="RisingNotesErrorConstants.SongPublishRequestHasNoLogo"/>
public class SongPublishRequestHasNoLogoException : BadRequestException
{
    public SongPublishRequestHasNoLogoException(string message) 
        : base(message, RisingNotesErrorConstants.SongPublishRequestHasNoLogo)
    {
    }

    public SongPublishRequestHasNoLogoException(Guid songPublishRequestId) 
        : base($"Song publish request with id={songPublishRequestId} has no logo", RisingNotesErrorConstants.SongPublishRequestHasNoLogo)
    {
    }
}