using AutoMapper;
using Dal.Song;
using Dal.Song.Repository;
using Dal.SongPublish;
using Dal.SongPublish.Repository;
using Logic.File;
using Logic.Song;
using MainLib.TagLib;
using RisingNotesLib.Enums;

namespace Logic.SongPublish;

/// <inheritdoc />
public class SongPublishManager : ISongPublishManager
{
    private readonly ISongPublishRequestRepository _repository;
    private readonly IMapper _mapper;
    private readonly ISongRepository _songRepository;
    private readonly IFileManager _fileManager;
    private readonly ISongManager _songManager;

    public SongPublishManager(
        ISongPublishRequestRepository repository,
        IMapper mapper,
        ISongRepository songRepository,
        IFileManager fileManager,
        ISongManager songManager)
    {
        _repository = repository;
        _mapper = mapper;
        _songRepository = songRepository;
        _fileManager = fileManager;
        _songManager = songManager;
    }

    /// <inheritdoc />
    public Task<Guid> CreateAsync(SongPublishRequestDal request)
    {
        return _repository.InsertAsync(request);
    }

    /// <inheritdoc />
    public async Task ReplyAsUserAsync(Guid requestId, SongPublishRequestDal newRequest)
    {
        var request = await _repository.GetAsync(requestId);
        if (request.Status is PublishRequestStatus.Approved or PublishRequestStatus.Rejected or PublishRequestStatus.Revoked)
        {
            // кидать исключение, что заявка уже обработана и отдавать текущйи статус
        }
        
        if (!string.IsNullOrWhiteSpace(newRequest.Lyrics))
        {
            request.Lyrics = newRequest.Lyrics;
        }

        if (!string.IsNullOrWhiteSpace(newRequest.Name))
        {
            request.Name = newRequest.Name;
        }

        if (newRequest.SongFile != null)
        {
            var songFileId = await _fileManager.UploadAsync(newRequest.SongFile);
            request.SongFileId = songFileId;
        }

        if (newRequest.LogoFile != null)
        {
            var logoFileId = await _fileManager.UploadAsync(newRequest.LogoFile);
            request.LogoFileId = logoFileId;
        }

        request.Status = PublishRequestStatus.Review;
        await _repository.UpdateAsync(request);
    }

    /// <inheritdoc />
    public async Task ReplyAsAdminAsync(Guid requestId, PublishRequestStatus status, string comment)
    {
        var request = await _repository.GetAsync(requestId);
        if (request.Status is PublishRequestStatus.Approved or PublishRequestStatus.Rejected)
        {
            // кидать исключение, что заявка уже обработана и отдавать текущйи статус
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
            await _repository.UpdateAsync(request);

            return;
        }

        // Удаляем, чтобы место не занимать
        if (status is PublishRequestStatus.Rejected or PublishRequestStatus.Revoked)
        {
            // TODO: нужно сделать метод DeleteAsync для FileManager. После этого раскомментить
            // request.SongFile = null;
            // request.LogoFile = null;
        }

        request.Status = status;
        if (!string.IsNullOrWhiteSpace(comment))
        {
            request.ReviewerComment = comment;
        }

        await _repository.UpdateAsync(request);
    }
}