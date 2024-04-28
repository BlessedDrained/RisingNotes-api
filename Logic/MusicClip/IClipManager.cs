using Dal.File;
using Dal.MusicClip;

namespace Logic.MusicClip;

/// <summary>
/// Менеджер для <see cref="ClipDal"/>
/// </summary>
public interface IClipManager
{
    /// <summary>
    /// Создать клип
    /// </summary>
    Task<Guid> UploadAsync(ClipDal clip, FileDal clipFile, FileDal previewFile);

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
    /// Обновить
    /// </summary>
    Task UpdateAsync(ClipDal clip);

    /// <summary>
    /// Удалить
    /// </summary>
    Task DeleteAsync(Guid id);
} 