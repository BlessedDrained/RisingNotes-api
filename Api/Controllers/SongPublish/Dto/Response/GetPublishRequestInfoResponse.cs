using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.SongPublish.Dto.Response;

/// <summary>
/// Получить подробную информацию о заявке
/// </summary>
public record GetPublishRequestInfoResponse
{
    /// <summary>
    /// Название песни
    /// </summary>
    public string SongName { get; init; }

    /// <summary>
    /// Текст песни
    /// </summary>
    public string Lyrics { get; init; }
    
    /// <summary>
    /// Является ли инструментальной
    /// </summary>
    public bool Instrumental { get; init; }

    /// <summary>
    /// Список жанров
    /// </summary>
    public string[] GenreList { get; init; } = Array.Empty<string>();

    /// <summary>
    /// Список настроений
    /// </summary>
    public string[] VibeList { get; init; } = Array.Empty<string>();

    /// <summary>
    /// Список языков
    /// </summary>
    public string[] LanguageList { get; init; } = Array.Empty<string>();

    /// <summary>
    /// Файл с песней
    /// </summary>
    public FileContentResult SongFile { get; init; }
    
    /// <summary>
    /// Файл логотипа
    /// </summary>
    public FileContentResult LogoFile { get; init; }
}