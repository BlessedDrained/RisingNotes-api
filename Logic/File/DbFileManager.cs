using Dal.File;
using Dal.File.Repository;
using MainLib.Logging;
using Microsoft.Extensions.Caching.Memory;

namespace Logic.File;

/// <summary>
/// Хранилище файлов в БД
/// </summary>
public class DbFileManager : IFileManager
{
    private readonly IFileRepository _fileRepository;
    private readonly MemoryCache _memoryCache;

    public DbFileManager(IFileRepository fileRepository)
    {
        _fileRepository = fileRepository;
        _memoryCache = new MemoryCache(new MemoryCacheOptions());
    }

    /// <inheritdoc />
    public Task<Guid> UploadAsync(FileDal file)
    {
        using var log = new MethodLog(file);

        return _fileRepository.InsertAsync(file);
        // var fileId = await _fileRepository.InsertAsync(file);
        // if (file.StorageType == StorageType.Database)
        // {
        //     fileId = await _fileRepository.InsertAsync(file);
        // }
        // else
        // {
        //     var fileStorage = _fileStorageFactory.CreateService(file.StorageType);
        //     fileId = await fileStorage.UploadFileAsync(file);
        //     file.Content = Array.Empty<byte>();
        // }
        //
        // return fileId;
    }

    /// <inheritdoc />
    public async Task<FileDal> DownloadAsync(Guid id)
    {
        using var log = new MethodLog(id);
        var file = await _fileRepository.GetAsync(id);

        if (_memoryCache.TryGetValue(id, out FileDal cachedFile))
        {
            log.ReturnsValue(cachedFile);
            return cachedFile;
        }

        _memoryCache.Set(id, file, TimeSpan.FromMinutes(15));

        log.ReturnsValue(file);

        return file;
        // if (file.StorageType == StorageType.Database)
        // {
        //     return file;
        // }
        //
        // var fileStorage = _fileStorageFactory.CreateService(file.StorageType);
        // var fileContent = await fileStorage.DownloadFileAsync(id);
    }
}