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
    public async Task<FileDal> UploadSingleAsync(FileDal file)
    {
        using var log = new MethodLog(file);

        var id = await _fileRepository.InsertAsync(file);
        log.ReturnsValue(id);

        return file;
    }

    /// <inheritdoc />
    public Task<string> StartMultipartUploadingOperationAsync(string key)
    {
        throw new NotSupportedException();
    }

    /// <inheritdoc />
    public Task UploadFilePartAsync(string uploadId, string key, IFormFile file, int partNumber, bool isLastPart)
    {
        throw new NotSupportedException();
    }

    /// <inheritdoc />
    public Task CompleteMultipartUploadAsync(string uploadId)
    {
        throw new NotSupportedException();
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
    public Task<FilePart> DownloadFilePartAsync(Guid fileId, int partNumber)
    {
        throw new NotSupportedException();
    }
    
    /// <inheritdoc />
    public Task DeleteAsync(Guid id)
    {
        return _fileRepository.DeleteAsync(id);
    }

    /// <inheritdoc />
    public Task<FileMetadata> GetFileMetadataAsync(Guid fileId)
    {
        throw new NotSupportedException();
    }
}