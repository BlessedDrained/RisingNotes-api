using System.Buffers;
using Dal.File;
using Dal.MusicClip;
using Dal.MusicClip.Repository;
using Logic.File;
using MainLib.Enums;
using MainLib.Logging;
using RisingNotesLib.Exceptions;

namespace Logic.MusicClip;

/// <inheritdoc />
public class ClipManager : IClipManager
{
    private readonly IClipRepository _clipRepository;
    private readonly IFileManager _fileManager;

    public ClipManager(
        IClipRepository clipRepository,
        IFileManager fileManager)
    {
        _clipRepository = clipRepository;
        _fileManager = fileManager;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(ClipDal clip)
    {
        using var log = new MethodLog(clip);

        var clipAlreadyUploadedForTheSong = await _clipRepository.ExistsAsync(x => x.SongId == clip.SongId);
        if (clipAlreadyUploadedForTheSong)
        {
            throw new ClipHasAlreadyBeenLoadedForTheSongException(clip.SongId);
        }
        
        var id = await _clipRepository.InsertAsync(clip);

        log.ReturnsValue(id);
        return id;
    }

    /// <inheritdoc />
    public async Task<ClipDal> GetAsync(Guid id)
    {
        using var log = new MethodLog(id);

        var clip = await _clipRepository.GetAsync(id);

        log.ReturnsValue(clip);
        return clip;
    }

    /// <inheritdoc />
    public async Task<FileDal> GetPreviewAsync(Guid clipId)
    {
        using var log = new MethodLog(clipId);

        var clip = await _clipRepository.GetAsync(clipId);
        var file = await _fileManager.DownloadAsync(clip.PreviewFileId);

        log.ReturnsValue(file);
        return file;
    }

    /// <inheritdoc />
    public async Task<FileDal> GetFileAsync(Guid clipId)
    {
        using var log = new MethodLog(clipId);

        var clip = await _clipRepository.GetAsync(clipId);
        var fileMetadata = await _fileManager.GetFileMetadataAsync(clip.ClipFileId);

        if (fileMetadata.SizeBytes > 50 * 1024 * 1024 && fileMetadata.PartCount > 1)
        {
            throw new Exception("File is too large to be downloaded as single entity or has multiple parts. Download it as multipart one");
        }
        
        var file = await _fileManager.DownloadAsync(clip.ClipFileId);

        log.ReturnsValue(file);
        return file;
    }

    /// <inheritdoc />
    public async Task<FilePart> GetFilePartAsync(Guid clipId, int partNumber)
    {
        using var log = new MethodLog(clipId);

        var clip = await _clipRepository.GetAsync(clipId);
        var filePart = await _fileManager.DownloadFilePartAsync(clip.ClipFileId, partNumber);

        return filePart;
    }

    /// <inheritdoc />
    public async Task UpdateAsync(ClipDal clip)
    {
        using var log = new MethodLog(clip);
        
        await _clipRepository.UpdateAsync(clip);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid id)
    {
        using var log = new MethodLog(id);
        
        await _clipRepository.DeleteAsync(id);
    }

    /// <inheritdoc />
    public async Task<string> StartClipFileUpdateAsync(Guid authorId, Guid clipId)
    {
        using var methodLog = new MethodLog(authorId, clipId);

        var clip = await _clipRepository.GetAsync(clipId);

        if (clip.UploaderId != authorId)
        {
            // сделать нормальный тип исключения
            throw new Exception();
        }

        var uploadId = await _fileManager.StartMultipartUploadingOperationAsync(clipId.ToString());
        return uploadId;
    }

    /// <inheritdoc />
    public async Task UpdateClipFilePartAsync(string uploadId, Guid clipId, Guid authorId, IFormFile file, int partNumber, bool isLastPart)
    {
        using var methodLog = new MethodLog(uploadId, partNumber, isLastPart);

        var clip = await _clipRepository.GetAsync(clipId);
        if (clip.UploaderId != authorId)
        {
            throw new ClipDoesNotBelongToCurrentAuthorException(authorId, clipId);
        }
        
        await _fileManager.UploadFilePartAsync(uploadId, clipId.ToString(), file, partNumber, isLastPart);

        if (isLastPart)
        {
            await _fileManager.CompleteMultipartUploadAsync(uploadId);
        }
    }


    /// <inheritdoc />
    public async Task<FileMetadata> GetClipMetadataAsync(Guid clipId)
    {
        using var methodLog = new MethodLog(clipId);

        var clip = await _clipRepository.GetAsync(clipId);
        var metadata = await _fileManager.GetFileMetadataAsync(clip.ClipFileId);

        return metadata;
    }

    /// <inheritdoc />
    public async Task UpdatePreviewAsync(Guid authorId, Guid clipId, IFormFile file)
    {
        using var methodLog = new MethodLog(authorId, file);

        var clip = await _clipRepository.GetAsync(clipId);
        if (clip.UploaderId != authorId)
        {
            throw new ClipDoesNotBelongToCurrentAuthorException(authorId, clipId);
        }
        
        // в случае с лого размер будет не более 5мб или же не более ~5млн байт
        var buffer = ArrayPool<byte>.Shared.Rent((int)file.Length);
        try
        {
            var memoryStream = new MemoryStream(buffer);
            await file.CopyToAsync(memoryStream);
            var byteList = memoryStream.ToArray();
            
            var nameWithExtension = file.FileName;
            var extension = Path.GetExtension(nameWithExtension);
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(nameWithExtension);

            var fileDal = new FileDal()
            {
                // файл загружен в хранилище, поэтому тут не заполняем
                Content = byteList,
                Extension = extension,
                Name = fileNameWithoutExtension,
                StorageType = StorageType.S3Storage
            };

            await _fileManager.UploadSingleAsync(fileDal);
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(buffer);
        }
    }
}