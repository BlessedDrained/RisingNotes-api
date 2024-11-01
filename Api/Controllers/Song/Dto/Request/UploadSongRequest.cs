﻿using Api.Controllers.File.Dto.Request;
using Api.Validation;
using RisingNotesLib.Enums;

namespace Api.Controllers.Song.Dto.Request;

/// <summary>
/// Модель запроса на создание трека
/// </summary>
public record UploadSongRequest
{
    /// <summary>
    /// Название
    /// </summary>
    // [Required]
    public string SongName { get; init; }

    /// <summary>
    /// Список жанров песни
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
    /// Является ли инструментальной
    /// </summary>
    public bool Instrumental { get; init; }

    /// <summary>
    /// Имеет ли текст
    /// </summary>
    // [Required]
    public bool HasLyrics { get; init; }

    /// <summary>
    /// Текст песни
    /// </summary>
    // TODO: добавить ограничение на длину
    public string Lyrics { get; init; }

    /// <summary>
    /// Содержит ненормативную лексику и т.д
    /// </summary>
    // [Required]
    public bool Explicit { get; init; }
    
    // TODO: сделать валидацию на положительное число
    public int DurationMsec { get; init; }
    
    // /// <summary>
    // /// Файл с треком
    // /// </summary>
    // // [Required]
    // [MaxFileSize(30720)]
    // [Obsolete(null, error: true)]
    // public UploadFileRequest SongFile { get; init; }

    // /// <summary>
    // /// Лого трека
    // /// </summary>
    // [MaxFileSize(2048)]
    // [Obsolete(null, error: true)]
    // public UploadFileRequest SongLogo { get; init; }
}