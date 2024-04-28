using Amazon.S3;
using Amazon.S3.Model;
using Dal.File;
using Dal.File.Repository;
using Dal.File.YandexDisk;
using MainLib.Enums;
using RisingNotesLib.Exceptions;

namespace Logic.File;

/// <summary>
/// Менеджер для <see cref="FileDal"/> с хранением файлов в s3 yandex
/// </summary>
public class YandexFileManager : IFileManager
{
    private readonly IFileRepository _fileRepository;
    private readonly IS3ClientFactory _s3ClientFactory;

    public YandexFileManager(IFileRepository fileRepository, IS3ClientFactory s3ClientFactory)
    {
        _fileRepository = fileRepository;
        _s3ClientFactory = s3ClientFactory;
    }

    /// <inheritdoc />
    public async Task<Guid> UploadAsync(FileDal file)
    {
        using var client = _s3ClientFactory.CreateClient();

        var fileId = Guid.NewGuid();
        var request = new PutObjectRequest()
        {
            BucketName = "rising-notes",
            Key = fileId.ToString(),
            InputStream = new MemoryStream(file.Content)
        };

        try
        {
            await client.PutObjectAsync(request);
        }
        catch
        {
            throw new S3FileUploadException("Error occured on trying to upload file");
        }

        file.Id = fileId;
        await _fileRepository.InsertAsync(file);
        
        return fileId;
    }

    /// <inheritdoc />
    public async Task<FileDal> DownloadAsync(Guid id)
    {
        var file = await _fileRepository.GetAsync(id);
        using var client = _s3ClientFactory.CreateClient();

        GetObjectResponse response;
        try
        {
            response = await client.GetObjectAsync("rising-notes", id.ToString());
        }
        catch
        {
            throw new S3FileDownloadException(StorageType.YandexDisk);
        }

        var buffer = new byte[response.ResponseStream.Length];
        file.Content = buffer;

        await using var stream = new BufferedStream(response.ResponseStream);
        _ = await stream.ReadAsync(buffer);

        return file;
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid id)
    {
        await _fileRepository.DeleteAsync(id);

        using var client = _s3ClientFactory.CreateClient();

        try
        {
            await client.DeleteObjectAsync("rising-notes", id.ToString());
        }
        catch (DeleteObjectsException e)
        {
            throw new S3FileDeleteException(e.Message);
        }
    }
}