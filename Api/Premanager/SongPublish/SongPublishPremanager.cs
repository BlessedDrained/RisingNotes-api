using Api.Controllers.SongPublish.Dto.Request;
using Api.Controllers.SongPublish.Dto.Response;
using AutoMapper;
using Dal.Author.Repository;
using Dal.SongPublish;
using Dal.SongPublish.Repository;
using Logic.SongPublish;
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
        var requestDal = new SongPublishRequestDal()
        {
            Status = PublishRequestStatus.Review
        };

        requestDal = _mapper.Map(request, requestDal);

        var author = await _authorRepository.GetByUserIdAsync(userId);
        requestDal.AuthorId = author.Id;

        var id = await _songPublishManager.CreateAsync(requestDal);

        var response = new CreateSongPublishRequestResponse()
        {
            Id = id
        };
        return response;
    }

    /// <inheritdoc />
    public async Task ReplyRequestAsUserAsync(Guid requestId, ReplyToRequestAsUserRequest request)
    {
        var requestDal = _mapper.Map<SongPublishRequestDal>(request);

        await _songPublishManager.ReplyAsUserAsync(requestId, requestDal);
    }

    /// <inheritdoc />
    public async Task<GetPublishRequestShortInfoListResponse> GetListAsync(GetPublishRequestListRequest request, bool isAdmin)
    {
        var filter = _mapper.Map<GetPublishRequestListFilterModel>(request);

        var result = await _songPublishRequestRepository.GetListAsync(filter, isAdmin);

        var responseList = _mapper.Map<List<GetPublishRequestShortInfoResponse>>(result);

        var response = new GetPublishRequestShortInfoListResponse()
        {
            PublishRequestShortInfoList = responseList
        };

        return response;
    }
}