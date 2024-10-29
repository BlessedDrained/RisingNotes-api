using MainLib.CustomException;
using RisingNotesLib.Constant;
using RisingNotesLib.Enums;

namespace RisingNotesLib.Exceptions;

/// <summary>
/// Текущий статус заявки не позволяет вносить изменения пользователю
/// </summary>
public class CurrentSongPublishRequestStatusDoesNotAllowUserInteractionException : BadRequestException
{
    public CurrentSongPublishRequestStatusDoesNotAllowUserInteractionException(string message) 
        : base(message, RisingNotesErrorConstants.CurrentSongPublishRequestStatusDoesNotAllowUserInteraction)
    {
    }

    public CurrentSongPublishRequestStatusDoesNotAllowUserInteractionException(PublishRequestStatus status) 
        : base($"Current request status of {status} does not allow user interaction", RisingNotesErrorConstants.CurrentSongPublishRequestStatusDoesNotAllowUserInteraction)
    {
    }
}