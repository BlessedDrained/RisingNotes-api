using System.Net;
using Amazon.S3.Model;
using Dal.File;
using Dal.File.Repository;
using Dal.File.YandexDisk;
using MainLib.Dal.Exception;

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

        var response = await client.PutObjectAsync(request);

        if ((int)response.HttpStatusCode >= 400)
        {
            // TODO: сделать кастомное исключение при загрузке файла
            throw new Exception();
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
        var response = await client.GetObjectAsync("rising-notes", id.ToString());

        if ((int) response.HttpStatusCode >= (int) HttpStatusCode.BadRequest)
        {
            // сделать кастомное исключение, что файл не найден
            throw new Exception();
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
        var response = await client.DeleteObjectAsync("rising-notes", id.ToString());
        
        if ((int)response.HttpStatusCode >= (int) HttpStatusCode.BadRequest)
        {
            // TODO: сделать кастомное исключение, если файл не был удален
            throw new Exception();
        }
    }
}