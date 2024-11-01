﻿namespace Api.Controllers.Playlist.Dto.Response;

/// <summary>
/// Мо
/// </summary>
public record GetPlaylistInfoResponse
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; init; }
    
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; init; }
    
    /// <summary>
    /// Является ли приватным
    /// </summary>
    public bool IsPrivate { get; init; }
}