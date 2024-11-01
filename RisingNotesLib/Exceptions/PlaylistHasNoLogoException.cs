﻿using MainLib.CustomException;
using RisingNotesLib.Constant;

namespace RisingNotesLib.Exceptions;

public class PlaylistHasNoLogoException : BadRequestException
{
    public PlaylistHasNoLogoException(Guid songId) : base($"Song with id={songId} has no logo", RisingNotesErrorConstants.PlaylistHasNoLogo)
    {
    }
}