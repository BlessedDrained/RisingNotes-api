using Dal.File;
using Dal.ShortVideo;

namespace Logic.ShortVideo;

/// <summary>
/// Менеджер для <see cref="ShortVideoDal"/>
/// </summary>
public interface IShortVideoManager
{
    /// <summary>
    /// Создать клип
    /// </summary>
    Task<Guid> UploadAsync(ShortVideoDal clip, FileDal clipFile, FileDal previewFile);

    /// <summary>
    /// Получить по id
    /// </summary>
    Task<ShortVideoDal> GetAsync(Guid id);

    /// <summary>
    /// Получить файл превью
    /// </summary>
    Task<FileDal> GetPreviewAsync(Guid clipId);

    /// <summary>
    /// Получить файл с клипом
    /// </summary>
    Task<FileDal> GetFileAsync(Guid clipId);

    /// <summary>
    /// Обновить
    /// </summary>
    Task UpdateAsync(ShortVideoDal clip);

    /// <summary>
    /// Удалить
    /// </summary>
    Task DeleteAsync(Guid id);
}