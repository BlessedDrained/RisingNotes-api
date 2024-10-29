using Api.Controllers.SongPublish.Dto.Request;
using Api.Controllers.SongPublish.Dto.Response;
using AutoMapper;
using Dal.Author.Repository;
using Dal.SongPublish;
using Dal.SongPublish.Repository;
using Logic.SongPublish;
using MainLib.Logging;
using RisingNotesLib.Enums;
using RisingNotesLib.Models;

namespace Api.Premanager.SongPublish;

/// <inheritdoc />
public class SongPublishPremanager : ISongPublishPremanager
{
    private readonly ISongPublishManager _songPublishManager;
    private readonly IMapper _mapper;
    private readonly IAuthorRepository _authorRepository;
    private readonly ISongPublishRequestRepository _songPublishRequestRepository;

    /// <summary>
    /// 
    /// </summary>
    public SongPublishPremanager(
        ISongPublishManager songPublishManager,
        IMapper mapper,
        IAuthorRepository authorRepository,
        ISongPublishRequestRepository songPublishRequestRepository)
    {
        _songPublishManager = songPublishManager;
        _mapper = mapper;
        _authorRepository = authorRepository;
        _songPublishRequestRepository = songPublishRequestRepository;
    }

    /// <inheritdoc />
    public async Task<CreateSongPublishRequestResponse> CreateAsync(Guid userId, CreateSongPublishRequestRequest request)
    {
        using var log = new MethodLog(userId, request);
        var requestDal = new SongPublishRequestDal()
        {
            Status = PublishRequestStatus.NeedsSongFile
        };

        requestDal = _mapper.Map(request, requestDal);

        var author = await _authorRepository.GetByUserIdAsync(userId);
        requestDal.AuthorId = author.Id;

        var id = await _songPublishManager.CreateAsync(requestDal);

        var response = new CreateSongPublishRequestResponse()
        {
            Id = id
        };
        log.ReturnsValue(response);
        return response;
    }

    /// <inheritdoc />
    public async Task ReplyRequestAsUserAsync(Guid requestId, ReplyToRequestAsUserRequest request)
    {
        using var log = new MethodLog(requestId, request);
        var requestDal = _mapper.Map<SongPublishRequestDal>(request);

        await _songPublishManager.ReplyAsUserAsync(requestId, requestDal);
    }

    /// <inheritdoc />
    public async Task<GetPublishRequestShortInfoListResponse> GetListAsync(GetPublishRequestListRequest request, Guid authorId)
    {
        using var log = new MethodLog(request, authorId);
        var filter = _mapper.Map<GetPublishRequestListFilterModel>(request);

        var result = await _songPublishRequestRepository.GetListAsync(filter, authorId);

        var responseList = _mapper.Map<List<GetPublishRequestShortInfoResponse>>(result);

        var response = new GetPublishRequestShortInfoListResponse()
        {
            PublishRequestShortInfoList = responseList
        };

        log.ReturnsValue(response);
        return response;
    }

    /// <inheritdoc />
    public async Task<GetPublishRequestInfoResponse> GetFullInfoAsync(Guid id)
    {
        using var log = new MethodLog(id);
        var result = await _songPublishRequestRepository.GetAsync(id);

        var response = _mapper.Map<GetPublishRequestInfoResponse>(result);

        log.ReturnsValue(response);
        return response;
    }

    /// <inheritdoc />
    public async Task<GetPublishRequestShortInfoListResponse> GetForReviewListAsync()
    {
        using var log = new MethodLog();
        var result = await _songPublishRequestRepository
            .GetListAsync(x => x.Status == PublishRequestStatus.Review);

        var responseList = _mapper.Map<List<GetPublishRequestShortInfoResponse>>(result);

        var response = new GetPublishRequestShortInfoListResponse()
        {
            PublishRequestShortInfoList = responseList
        };
        log.ReturnsValue(responseList);

        return response;
    }
}