namespace RisingNotesLib.Enums;

/// <summary>
/// Тип logo owner
/// </summary>
public enum LogoOwnerType
{
    Unknown = 0,

    User = 1,

    Song = 2,

    Playlist = 3
}

public static class LogoTypeExtensions
{
    public static string GetLogoType(this LogoOwnerType logoOwnerType)
    {
        return logoOwnerType
            .ToString()
            .ToLower();
    }
}