using Amazon.S3;
using Amazon.S3.Model;
using Dal.File;
using Dal.File.Repository;
using Dal.File.S3;
using MainLib.Enums;
using MainLib.Logging;
using RisingNotesLib.Exceptions;

namespace Logic.File;

/// <summary>
/// Менеджер для <see cref="FileDal"/> с хранением файлов в s3 yandex
/// </summary>
public class S3FileManager : IFileManager
{
    private readonly IFileRepository _fileRepository;
    private readonly IS3ClientFactory _s3ClientFactory;

    public S3FileManager(IFileRepository fileRepository, IS3ClientFactory s3ClientFactory)
    {
        _fileRepository = fileRepository;
        _s3ClientFactory = s3ClientFactory;
    }

    /// <inheritdoc />
    public async Task<FileDal> UploadSingleAsync(FileDal file)
    {
        using var client = _s3ClientFactory.CreateClient();

        var tempContent = file.Content;
        file.Content = null;
        try
        {
            await _fileRepository.InsertAsync(file);
        }
        catch
        {
            await _fileRepository.DeleteAsync(file.Id);
            throw;
        }
        
        file.Content = tempContent;
        
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
            throw new S3FileUploadException("Error occured while trying to upload file");
        }
        
        return file;
    }

    /// <inheritdoc />
    public async Task<string> StartMultipartUploadingOperationAsync(string key)
    {
        using var log = new MethodLog();

        using var client = _s3ClientFactory.CreateClient();

        var request = new InitiateMultipartUploadRequest()
        {
            BucketName = client.BucketName,
            Key = key,
        };
        var response = await client.InnerClient.InitiateMultipartUploadAsync(request);
        
        return response.UploadId;
    }

    /// <inheritdoc />
    public async Task UploadFilePartAsync(
        string uploadId, 
        string key, 
        IFormFile file, 
        int partNumber,
        bool isLastPart)
    {
        using var log = new MethodLog(uploadId, key);
        using var client = _s3ClientFactory.CreateClient();

        var request = new UploadPartRequest()
        {
            BucketName = client.BucketName,
            Key = key,
            UploadId = uploadId,
            PartNumber = partNumber,
            IsLastPart = isLastPart,
            // TODO: нужно сделать заполнение этой части. Пока что это будет ломаться PartNumber = 
        };

        await client.InnerClient.UploadPartAsync(request);

        // если с фронтенда сказали, что это последняя часть, то закрываем загрузку
        if (isLastPart)
        {
            await CompleteMultipartUploadAsync(uploadId);
        }
    }

    public async Task CompleteMultipartUploadAsync(string uploadId)
    {
        using var log = new MethodLog();
        using var client = _s3ClientFactory.CreateClient();

        var request = new CompleteMultipartUploadRequest()
        {
            BucketName = client.BucketName,
            Key = uploadId,
            UploadId = uploadId,
        };

        await client.InnerClient.CompleteMultipartUploadAsync(request);
    }

    /// <inheritdoc />
    public async Task<FileDal> DownloadAsync(Guid id)
    {
        var file = await _fileRepository.GetAsync(id);
        using var client = _s3ClientFactory.CreateClient();

        GetObjectResponse response;
        try
        {
            response = await client.InnerClient.GetObjectAsync(client.BucketName, file.Id.ToString());
        }
        catch
        {
            throw new S3FileDownloadException(StorageType.S3Storage);
        }

        using var ms = new MemoryStream();
        await response.ResponseStream.CopyToAsync(ms);
        file.Content = ms.ToArray();

        return file;
    }

    public async Task<FilePart> DownloadFilePartAsync(Guid fileId, int partNumber)
    {
        using var log = new MethodLog();
        using var client = _s3ClientFactory.CreateClient();

        var request = new GetObjectRequest()
        {
            BucketName = client.BucketName,
            Key = fileId.ToString(),
            PartNumber = partNumber
        };
        var response = await client.InnerClient.GetObjectAsync(request);

        var result = new FilePart()
        {
            FileId = fileId,
            FileStream = response.ResponseStream
        };

        return result;
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid id)
    {
        await _fileRepository.DeleteAsync(id);

        using var client = _s3ClientFactory.CreateClient();

        try
        {
            await client.InnerClient.DeleteObjectAsync(client.BucketName, id.ToString());
        }
        catch (DeleteObjectsException e)
        {
            throw new S3FileDeleteException(e.Message);
        }
    }

    /// <inheritdoc />
    public async Task<FileMetadata> GetFileMetadataAsync(Guid fileId)
    {
        var file = await _fileRepository.GetAsync(fileId);

        using var client = _s3ClientFactory.CreateClient();
        
        var response = await client.InnerClient.GetObjectMetadataAsync(client.BucketName, file.Id.ToString());
        var metadata = new FileMetadata()
        {
            SizeBytes = response.ContentLength,
            PartCount = response.PartsCount,
            ContentType = response.Headers.ContentType
        };

        return metadata;
    }
}