﻿namespace Api.Controllers.Clip.Dto.Response;

/// <summary>
/// Ответ на получение инфы о клипе
/// </summary>
public record GetClipInfoResponse
{
    /// <summary>
    /// Идентификатор клипа
    /// </summary>
    public Guid Id { get; init; }
    
    /// <summary>
    /// Название
    /// </summary>
    public string Title { get; init; }
    
    /// <summary>
    /// Идентификатор файла превью
    /// </summary>
    public Guid PreviewFileId { get; init; }
    
    /// <summary>
    /// Идентификатор файла клипа
    /// </summary>
    public Guid ClipFileId { get; init; }
    
    /// <summary>
    /// Идентификатор песни, для которой создан клип
    /// </summary>
    public Guid SongId { get; init; }
    
    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; init; }
    
    /// <summary>
    /// Идентификатор кто загрузил клип
    /// </summary>
    public Guid UploaderId { get; init; }
}