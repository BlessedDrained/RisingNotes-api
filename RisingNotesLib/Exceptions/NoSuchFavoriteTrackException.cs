using MainLib.CustomException;
using RisingNotesLib.Constant;

namespace RisingNotesLib.Exceptions;

/// <summary>
/// Такого трека нет в избранном
/// </summary>
public class NoSuchFavoriteTrackException : BadRequestException
{
    public NoSuchFavoriteTrackException(Guid songId) : base(
        $"Track with id={songId} is not present in favorite", 
        RisingNotesErrorConstants.NoSuchFavoriteTrack)
    {
    }
}