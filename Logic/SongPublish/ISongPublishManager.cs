using Dal.File;
using Dal.SongPublish;
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

    /// <summary>
    /// Загрузить логотип песни
    /// </summary>
    Task UpdateLogoAsync(Guid requestId, Guid authorId, IFormFile file);

    /// <summary>
    /// Начать операцию по обновлению файла трека большого размера
    /// </summary>
    Task<string> StartSongFileUpdateAsync(Guid authorId, Guid publishRequestId);

    /// <summary>
    /// Загрузить часть песни
    /// </summary>
    Task UpdateSongFilePartAsync(string uploadId, Guid publishRequestId, Guid authorId, IFormFile file, int partNumber, bool isLastPart);

    /// <summary>
    /// Получить файл логотипа
    /// </summary>
    Task<FileDal> GetLogoAsync(Guid requestId);

    /// <summary>
    /// Получить файл песни
    /// </summary>
    Task<FileDal> GetSongFileAsync(Guid requestId);
    
    // /// <summary>
    // /// Получить список заявок
    // /// </summary>
    // Task<List<SongPublishRequestDal>> GetListAsync(GetPublishRequestListFilterModel filter);
}