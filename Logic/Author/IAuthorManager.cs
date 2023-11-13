﻿using Dal.Author;
using Dal.Song;

namespace Logic.Author;

/// <summary>
/// Менеджер для <see cref="AuthorDal"/>
/// </summary>
public interface IAuthorManager
{
    /// <summary>
    /// Создать автора
    /// </summary>
    Task<Guid> CreateAsync(Guid userId, AuthorDal authorDal);

    /// <summary>
    /// Получить список треков автора
    /// </summary>
    Task<List<SongDal>> GetAuthorSongInfoListAsync(Guid authorId);
}