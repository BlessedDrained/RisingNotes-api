namespace Dal.File.YandexDisk;

/// <summary>
/// Сервис для взаимодействия с Яндекс диском
/// </summary>
public class YandexDiskStorage : IFileStorage
{
    /// <inheritdoc />
    public Task<Guid> UploadFileAsync(FileDal request)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<byte[]> DownloadFileAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}