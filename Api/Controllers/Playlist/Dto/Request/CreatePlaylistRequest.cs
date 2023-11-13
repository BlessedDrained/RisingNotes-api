using Api.Controllers.File.Dto.Request;

namespace Api.Controllers.Playlist.Dto.Request;

/// <summary>
/// Модель запроса на создание плейлиста
/// </summary>
public record CreatePlaylistRequest
{
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; init; }
    
    /// <summary>
    /// Файл логотипа
    /// </summary>
    public UploadFileRequest LogoFile { get; init; }
}