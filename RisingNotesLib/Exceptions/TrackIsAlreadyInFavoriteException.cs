using MainLib.CustomException;
using RisingNotesLib.Constant;

namespace RisingNotesLib.Exceptions;

/// <summary>
/// Трек уже есть в избранном
/// </summary>
public class TrackIsAlreadyInFavoriteException : BadRequestException
{
    public TrackIsAlreadyInFavoriteException(Guid songId) 
        : base($"Track with id={songId} is already in favorite", RisingNotesErrorConstants.TrackIsAlreadyInFavorite)
    {
    }
}