using Dal.File;

namespace Logic.File;

/// <summary>
/// Менеджер для <see cref="FileDal"/>
/// </summary>
public interface IFileManager
{
    /// <summary>
    /// Загрузить файл
    /// </summary>
    Task<Guid> UploadAsync(FileDal file);

    /// <summary>
    /// Скачать файл
    /// </summary>
    Task<FileDal> DownloadAsync(Guid id);

    /// <summary>
    /// Удалить файл
    /// </summary>
    Task DeleteAsync(Guid id);
}