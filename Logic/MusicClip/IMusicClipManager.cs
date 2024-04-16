using Dal.File;
using Dal.MusicClip;

namespace Logic.MusicClip;

/// <summary>
/// Менеджер для <see cref="MusicClipDal"/>
/// </summary>
public interface IMusicClipManager
{
    /// <summary>
    /// Создать клип
    /// </summary>
    Task<Guid> UploadAsync(MusicClipDal clip, FileDal clipFile, FileDal previewFile);

    /// <summary>
    /// Получить по id
    /// </summary>
    Task<MusicClipDal> GetAsync(Guid id);

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
    Task UpdateAsync(MusicClipDal clip);

    /// <summary>
    /// Удалить
    /// </summary>
    Task DeleteAsync(Guid id);
} 