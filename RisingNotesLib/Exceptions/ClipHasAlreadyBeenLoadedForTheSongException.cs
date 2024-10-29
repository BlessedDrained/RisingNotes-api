using MainLib.CustomException;
using RisingNotesLib.Constant;

namespace RisingNotesLib.Exceptions;

/// <summary>
/// Клип уже был загружен для данной песни
/// </summary>
public class ClipHasAlreadyBeenLoadedForTheSongException : BadRequestException
{
    public ClipHasAlreadyBeenLoadedForTheSongException(string message) 
        : base(message, RisingNotesErrorConstants.ClipHasAlreadyBeenLoadedForTheSongException)
    {
    }

    public ClipHasAlreadyBeenLoadedForTheSongException(Guid songId) 
        : base($"Clip has already been loaded for the song with id={songId}", RisingNotesErrorConstants.ClipHasAlreadyBeenLoadedForTheSongException)
    {
    }
}