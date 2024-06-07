using Dal.File;
using Dal.File.Repository;
using MainLib.Logging;

namespace Logic.File;

/// <summary>
/// Хранилище файлов в БД
/// </summary>
public class DbFileManager : IFileManager
{
    private readonly IFileRepository _fileRepository;

    public DbFileManager(IFileRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }

    /// <inheritdoc />
    public async Task<FileDal> UploadAsync(FileDal file)
    {
        using var log = new MethodLog(file);

        var id = await _fileRepository.InsertAsync(file);
        log.ReturnsValue(id);

        return file;
    }

    /// <inheritdoc />
    public async Task<FileDal> DownloadAsync(Guid id)
    {
        using var log = new MethodLog(id);

        var file = await _fileRepository.GetAsync(id);

        log.ReturnsValue(file);
        return file;
    }

    /// <inheritdoc />
    public Task DeleteAsync(Guid id)
    {
        return _fileRepository.DeleteAsync(id);
    }
}