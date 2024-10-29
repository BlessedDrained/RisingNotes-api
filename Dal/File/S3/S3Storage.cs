namespace Dal.File.S3;

/// <summary>
/// Сервис для взаимодействия с Яндекс диском
/// </summary>
public class S3Storage : IFileStorage
{
    /// <inheritdoc />
    public Task<Guid> UploadFileAsync(FileDal request)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<FileDal> DownloadFileAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}