namespace Dal.File;

/// <summary>
/// Сервис файлового хранилища
/// </summary>
public interface IFileStorage
{
    /// <summary>
    /// Загрузить файл в хранилище
    /// </summary>
    Task<Guid> UploadFileAsync(FileDal request);

    /// <summary>
    /// Скачать файл из хранилища
    /// </summary>
    Task<byte[]> DownloadFileAsync(Guid id);
}