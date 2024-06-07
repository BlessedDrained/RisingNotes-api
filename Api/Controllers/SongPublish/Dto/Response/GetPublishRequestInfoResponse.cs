using Microsoft.AspNetCore.Mvc;
using RisingNotesLib.Enums;

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
    /// 
    /// </summary>
    public Gender[] VocalGenderList { get; init; } = Array.Empty<Gender>();
    
    /// <summary>
    /// Идентификатор песни, если она выложена
    /// </summary>
    public Guid? PublishedSongId { get; init; }
    
    /// <summary>
    /// Продолжительность трека в секундах
    /// </summary>
    public int DurationMs { get; init; }

    /// <summary>
    /// Файл с песней
    /// </summary>
    public FileContentResult SongFile { get; init; }

    /// <summary>
    /// Файл логотипа
    /// </summary>
    public FileContentResult LogoFile { get; init; }
    
    /// <summary>
    /// Комментарий админа
    /// </summary>
    public string ReviewerComment { get; init; }
}