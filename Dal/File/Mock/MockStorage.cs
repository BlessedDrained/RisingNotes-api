using MainLib.Enums;

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
    public Task<FileDal> DownloadFileAsync(Guid id)
    {
        return Task.FromResult(new FileDal()
        {
            Content = Array.Empty<byte>(),
            Extension = ".test",
            Name = "test",
            StorageType = StorageType.YandexDisk,
            Id = Guid.NewGuid()
        });
    }
}