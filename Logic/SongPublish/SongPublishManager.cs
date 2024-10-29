using System.Buffers;
using System.IO.Pipelines;
using AutoMapper;
using Dal.File;
using Dal.Song;
using Dal.Song.Repository;
using Dal.SongPublish;
using Dal.SongPublish.Repository;
using Logic.File;
using MainLib.Logging;
using MainLib.TagLib;
using RisingNotesLib.Enums;
using RisingNotesLib.Exceptions;

namespace Logic.SongPublish;

/// <inheritdoc />
public class SongPublishManager : ISongPublishManager
{
    private readonly ISongPublishRequestRepository _repository;
    private readonly IMapper _mapper;
    private readonly ISongRepository _songRepository;
    private readonly IFileManager _fileManager;

    public SongPublishManager(
        ISongPublishRequestRepository repository,
        IMapper mapper,
        ISongRepository songRepository,
        IFileManager fileManager)
    {
        _repository = repository;
        _mapper = mapper;
        _songRepository = songRepository;
        _fileManager = fileManager;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(SongPublishRequestDal request)
    {
        var id = await _repository.InsertAsync(request);
        return id;
    }

    /// <inheritdoc />
    public async Task ReplyAsUserAsync(Guid requestId, SongPublishRequestDal newRequest)
    {
        using var log = new MethodLog(requestId, newRequest);
        
        var request = await _repository.GetAsync(requestId);
        if (request.Status is PublishRequestStatus.Approved or PublishRequestStatus.Rejected or PublishRequestStatus.Revoked)
        {
            throw new CurrentSongPublishRequestStatusDoesNotAllowUserInteractionException(request.Status);
        }

        if (!string.IsNullOrWhiteSpace(newRequest.Lyrics))
        {
            request.Lyrics = newRequest.Lyrics;
        }

        if (!string.IsNullOrWhiteSpace(newRequest.Name))
        {
            request.Name = newRequest.Name;
        }

        request.LanguageList = newRequest.LanguageList;
        request.VocalGenderList = newRequest.VocalGenderList;
        request.GenreList = newRequest.GenreList;
        request.VibeList = newRequest.VibeList;
        request.Instrumental = newRequest.Instrumental;

        if (newRequest.SongFile != null)
        {
            var file = TagLib.File.Create(new FileAbstraction($"{request.SongFile!.Name}.{request.SongFile.Extension}", request.SongFile.Content));
            request.DurationMs = Convert.ToInt32(file.Properties.Duration.TotalMilliseconds);
            await _fileManager.UploadSingleAsync(newRequest.SongFile);
            request.SongFileId = newRequest.SongFileId;
        }

        if (newRequest.LogoFile != null)
        {
            await _fileManager.UploadSingleAsync(newRequest.LogoFile);
            request.LogoFileId = newRequest.LogoFile.Id;
        }

        if (request.SongFileId.HasValue && request.LogoFileId.HasValue)
        {
            request.Status = PublishRequestStatus.Review;
        }
        
        await _repository.UpdateAsync(request);
    }

    /// <inheritdoc />
    public async Task ReplyAsAdminAsync(Guid requestId, PublishRequestStatus status, string comment)
    {
        using var log = new MethodLog(requestId, status, comment);

        var request = await _repository.GetAsync(requestId);
        if (request.Status is PublishRequestStatus.Approved or PublishRequestStatus.Rejected)
        {
            throw new Exception("Publish request has already been handled");
        }

        // Одобрили на данном этапе заявку
        if (status == PublishRequestStatus.Approved)
        {
            // нужно смаппить заявку в песню и опубликовать ее
            var song = _mapper.Map<SongDal>(request);
            var songFile = await _fileManager.DownloadAsync(song.SongFileId);
            var file = TagLib.File.Create(new FileAbstraction($"{songFile.Name}.{songFile.Extension}", songFile.Content));
            song.DurationMsec = Convert.ToInt32(file.Properties.Duration.TotalMilliseconds);

            await _songRepository.InsertAsync(song);
            request.Song = song;
            request.Status = PublishRequestStatus.Approved;
            await _repository.UpdateAsync(request);

            return;
        }

        if (status is PublishRequestStatus.Revoked)
        {
            if (request.SongFileId.HasValue)
            {
                await _fileManager.DeleteAsync(request.SongFileId.Value);
            }

            if (request.LogoFileId.HasValue)
            {
                await _fileManager.DeleteAsync(request.LogoFileId.Value);
            }
        }
        
        request.Status = status;
        if (!string.IsNullOrWhiteSpace(comment))
        {
            request.ReviewerComment = comment;
        }

        await _repository.UpdateAsync(request);
    }

    /// <inheritdoc />
    public async Task UpdateLogoAsync(Guid requestId, Guid authorId, IFormFile file)
    {
        using var log = new MethodLog(requestId);

        var request = await _repository.GetAsync(requestId);
        if (request.AuthorId != authorId)
        {
            throw new Exception();
        }
        
        // размер логотипа не более 5мб, поэтому не будет такого кейса, что массив окажется меньше длины файла
        await using var openReadStream = file.OpenReadStream();
        await using var stream = new MemoryStream();
        await openReadStream.CopyToAsync(stream);

        var nameWithExtension = file.FileName;
        var name = Path.GetFileName(nameWithExtension);
        var extension = Path.GetExtension(nameWithExtension);
        var fileDal = new FileDal()
        {
            Content = stream.ToArray(),
            Extension = extension,
            Name = name
        };
        
        fileDal = await _fileManager.UploadSingleAsync(fileDal);
        request.LogoFileId = fileDal.Id;
        await _repository.UpdateAsync(request);
    }

    /// <inheritdoc />
    public async Task<string> StartSongFileUpdateAsync(Guid authorId, Guid publishRequestId)
    {
        using var methodLog = new MethodLog(authorId, publishRequestId);

        var clip = await _repository.GetAsync(publishRequestId);

        if (clip.AuthorId != authorId)
        {
            // сделать нормальный тип исключения
            throw new Exception();
        }

        var uploadId = await _fileManager.StartMultipartUploadingOperationAsync(publishRequestId.ToString());
        return uploadId;
    }

    /// <inheritdoc />
    public async Task UpdateSongFilePartAsync(string uploadId, Guid publishRequestId, Guid authorId, IFormFile file, int partNumber, bool isLastPart)
    {
        using var methodLog = new MethodLog(uploadId, partNumber, isLastPart);

        var clip = await _repository.GetAsync(publishRequestId);
        if (clip.AuthorId != authorId)
        {
            throw new ClipDoesNotBelongToCurrentAuthorException(authorId, publishRequestId);
        }
        
        await _fileManager.UploadFilePartAsync(uploadId, publishRequestId.ToString(), file, partNumber, isLastPart);

        if (isLastPart)
        {
            await _fileManager.CompleteMultipartUploadAsync(uploadId);
        }
    }

    /// <inheritdoc />
    public async Task<FileDal> GetLogoAsync(Guid requestId)
    {
        using var log = new MethodLog(requestId);

        var request = await _repository.GetAsync(requestId);

        if (!request.LogoFileId.HasValue)
        {
            throw new SongPublishRequestHasNoLogoException(requestId);
        }
        
        var file = await _fileManager.DownloadAsync(request.LogoFileId.Value);

        return file;
    }

    /// <inheritdoc />
    public async Task<FileDal> GetSongFileAsync(Guid requestId)
    {
        using var log = new MethodLog(requestId);

        var request = await _repository.GetAsync(requestId);

        if (!request.SongFileId.HasValue)
        {
            throw new SongPublishRequestHasNoFileException(requestId);
        }
        
        var file = await _fileManager.DownloadAsync(request.SongFileId.Value);

        return file;
    }
}