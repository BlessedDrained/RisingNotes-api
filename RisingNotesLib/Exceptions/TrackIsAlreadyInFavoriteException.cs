using MainLib.CustomException;

namespace RisingNotesLib.Exceptions;

/// <summary>
/// Трек уже есть в избранном
/// </summary>
public class TrackIsAlreadyInFavoriteException : BadRequestException
{
    public TrackIsAlreadyInFavoriteException(Guid songId) : base($"Track with id={songId} is already in favorite")
    {
    }
}