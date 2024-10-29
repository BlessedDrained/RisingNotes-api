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
    Task<FileDal> UploadSingleAsync(FileDal file);

    /// <summary>
    /// Начать операцию по частичной загрузке файла
    /// </summary>
    /// <returns></returns>
    Task<string> StartMultipartUploadingOperationAsync(string key);

    /// <summary>
    /// Загрузить часть файла
    /// </summary>
    Task UploadFilePartAsync(string uploadId, 
        string key, 
        IFormFile file, 
        int partNumber,
        bool isLastPart);
    
    /// <summary>
    /// Закончить операцию по частичной загрузке файла
    /// </summary>
    Task CompleteMultipartUploadAsync(string uploadId);

    /// <summary>
    /// Скачать файл
    /// </summary>
    Task<FileDal> DownloadAsync(Guid id);

    /// <summary>
    /// Загрузить часть файла
    /// </summary>
    /// <returns></returns>
    Task<FilePart> DownloadFilePartAsync(Guid fileId, int partNumber);

    /// <summary>
    /// Удалить файл
    /// </summary>
    Task DeleteAsync(Guid id);

    /// <summary>
    /// Получить метаданные о файле: размер в байтах, contentType
    /// </summary>
    Task<FileMetadata> GetFileMetadataAsync(Guid fileId);
}