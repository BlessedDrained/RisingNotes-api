using MainLib.CustomException;
using RisingNotesLib.Constant;

namespace RisingNotesLib.Exceptions;

public class SongHasNoLogoException : BadRequestException
{
    public SongHasNoLogoException(Guid songId) : base($"Song with id={songId} has no logo", RisingNotesErrorConstants.SongHasNoLogo)
    {
    }
}