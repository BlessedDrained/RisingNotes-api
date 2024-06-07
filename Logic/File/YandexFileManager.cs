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
    private readonly IYandexS3ClientFactory _yandexS3ClientFactory;

    public YandexFileManager(IFileRepository fileRepository, IYandexS3ClientFactory yandexS3ClientFactory)
    {
        _fileRepository = fileRepository;
        _yandexS3ClientFactory = yandexS3ClientFactory;
    }

    /// <inheritdoc />
    public async Task<FileDal> UploadAsync(FileDal file)
    {
        using var client = _yandexS3ClientFactory.CreateClient();

        var tempContent = file.Content;
        file.Content = null;
        try
        {
            await _fileRepository.InsertAsync(file);
            file.Content = tempContent;
        }
        catch
        {
            await _fileRepository.DeleteAsync(file.Id);
            throw;
        }
        
        var request = new PutObjectRequest()
        {
            BucketName = client.BucketName,
            Key = file.Id.ToString(),
            InputStream = new MemoryStream(file.Content)
        };

        try
        {
            await client.InnerClient.PutObjectAsync(request);
            file.Content = null;
        }
        catch
        {
            throw new S3FileUploadException("Error occured on trying to upload file");
        }
        
        return file;
    }

    /// <inheritdoc />
    public async Task<FileDal> DownloadAsync(Guid id)
    {
        var file = await _fileRepository.GetAsync(id);
        using var client = _yandexS3ClientFactory.CreateClient();

        GetObjectResponse response;
        try
        {
            response = await client.InnerClient.GetObjectAsync(client.BucketName, file.Id.ToString());
        }
        catch
        {
            throw new S3FileDownloadException(StorageType.YandexDisk);
        }

        using (var ms = new MemoryStream())
        {
            await response.ResponseStream.CopyToAsync(ms);
            file.Content = ms.ToArray();
        }

        return file;
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid id)
    {
        await _fileRepository.DeleteAsync(id);

        using var client = _yandexS3ClientFactory.CreateClient();

        try
        {
            await client.InnerClient.DeleteObjectAsync(client.BucketName, id.ToString());
        }
        catch (DeleteObjectsException e)
        {
            throw new S3FileDeleteException(e.Message);
        }
    }
}