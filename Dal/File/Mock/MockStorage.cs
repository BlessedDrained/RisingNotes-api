namespace Dal.File.Mock;

/// <summary>
/// Моковый сервис для загрузки файлов
/// </summary>
public class MockStorage : IFileStorage
{
    /// <inheritdoc />
    public Task<Guid> UploadFileAsync(FileDal request)
    {
        return Task.FromResult(Guid.NewGuid());
    }

    /// <inheritdoc />
    public Task<byte[]> DownloadFileAsync(Guid id)
    {
        return Task.FromResult(Array.Empty<byte>());
    }
}