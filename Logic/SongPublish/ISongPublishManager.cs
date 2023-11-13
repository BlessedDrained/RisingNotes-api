﻿using Dal.SongPublish;
using RisingNotesLib.Enums;

namespace Logic.SongPublish;

/// <summary>
/// 
/// </summary>
public interface ISongPublishManager
{
    /// <summary>
    /// Создать заявку
    /// </summary>
    Task<Guid> CreateAsync(SongPublishRequestDal request);

    /// <summary>
    /// Ответить на заявку от лица пользователя
    /// </summary>
    Task ReplyAsUserAsync(Guid requestId, SongPublishRequestDal newRequest);
    
    /// <summary>
    /// Ответить на заявку от имени админа
    /// </summary>
    Task ReplyAsAdminAsync(Guid requestId, PublishRequestStatus status, string comment);

    // /// <summary>
    // /// Получить список заявок
    // /// </summary>
    // Task<List<SongPublishRequestDal>> GetListAsync(GetPublishRequestListFilterModel filter);
}