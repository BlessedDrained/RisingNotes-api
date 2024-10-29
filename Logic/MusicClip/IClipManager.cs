using Dal.File;
using Dal.MusicClip;
using Logic.File;

namespace Logic.MusicClip;

/// <summary>
/// Менеджер для <see cref="ClipDal"/>
/// </summary>
public interface IClipManager
{
    /// <summary>
    /// Создать клип
    /// </summary>
    Task<Guid> CreateAsync(ClipDal clip);

    /// <summary>
    /// Получить по id
    /// </summary>
    Task<ClipDal> GetAsync(Guid id);

    /// <summary>
    /// Получить файл превью
    /// </summary>
    Task<FileDal> GetPreviewAsync(Guid clipId);

    /// <summary>
    /// Получить файл с клипом
    /// </summary>
    Task<FileDal> GetFileAsync(Guid clipId);

    /// <summary>
    /// Получить часть файла с клипом
    /// </summary>
    Task<FilePart> GetFilePartAsync(Guid clipId, int partNumber);

    /// <summary>
    /// Обновить 
    /// </summary>
    Task UpdateAsync(ClipDal clip);

    /// <summary>
    /// Обновить превью клипа
    /// </summary>
    Task UpdatePreviewAsync(Guid authorId, Guid clipId, IFormFile file);

    /// <summary>
    /// Удалить
    /// </summary>
    Task DeleteAsync(Guid id);

    /// <summary>
    /// Начать операцию обновления
    /// </summary>
    Task<string> StartClipFileUpdateAsync(Guid authorId, Guid clipId);

    /// <summary>
    /// Загрузить часть клипа
    /// </summary>
    Task UpdateClipFilePartAsync(string uploadId, Guid clipId, Guid authorId, IFormFile file, int partNumber, bool isLastPart);

    /// <summary>
    /// Получить метаинформацию о клипе
    /// </summary>
    /// <param name="clipId"></param>
    /// <returns></returns>
    Task<FileMetadata> GetClipMetadataAsync(Guid clipId);
} 