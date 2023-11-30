using MainLib.CustomException;

namespace RisingNotesLib.Exceptions;

public class SongHasNoLogoException : BadRequestException
{
    public SongHasNoLogoException(Guid songId) : base($"Song with id={songId} has no logo")
    {
    }
}