using Api.Controllers.Author.Dto.Request;
using Api.Controllers.Author.Dto.Response;
using Api.Controllers.Song.Dto.Response;
using Api.Controllers.Subscription.Dto.Response;
using AutoMapper;
using Dal.Author;
using Dal.Author.Repository;
using Dal.BaseUser.Repository;
using Logic.Author;
using MainLib.Logging;
using RisingNotesLib.Models;

namespace Api.Premanager.Author;

/// <inheritdoc />
public class AuthorPremanager : IAuthorPremanager
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    private readonly IAuthorManager _authorManager;
    private readonly IUserRepository _userRepository;

    /// 
    public AuthorPremanager(
        IAuthorRepository authorRepository,
        IMapper mapper,
        IAuthorManager authorManager,
        IUserRepository userRepository)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
        _authorManager = authorManager;
        _userRepository = userRepository;
    }

    /// <inheritdoc />
    public async Task<Guid> MakeAuthorAsync(MakeAuthorRequest request)
    {
        var author = _mapper.Map<AuthorDal>(request);

        var id = await _authorManager.CreateAsync(request.UserId, author);

        return id;
    }

    /// <inheritdoc />
    public async Task<GetAuthorShortInfoResponse> GetShortInfoAsync(string authorName)
    {
        var author = await _authorRepository.GetShortInfoAsync(authorName);
        var response = _mapper.Map<GetAuthorShortInfoResponse>(author);

        return response;
    }

    /// <inheritdoc />
    public async Task<GetAuthorInfoResponse> GetInfoAsync(string authorName)
    {
        var author = await _authorRepository.GetInfoAsync(authorName);
        var response = _mapper.Map<GetAuthorInfoResponse>(author);

        return response;
    }

    /// <inheritdoc />
    public async Task<GetAuthorInfoResponse> GetInfoAsync(Guid authorId)
    {
        var author = await _authorRepository.GetAsync(authorId);
        var response = _mapper.Map<GetAuthorInfoResponse>(author);

        return response;
    }

    /// <inheritdoc />
    public async Task<GetAuthorListResponse> GetListAsync(GetAuthorListRequest request)
    {
        var filter = _mapper.Map<GetAuthorListFilterModel>(request);
        var authorList = await _authorRepository.GetListAsync(filter);

        var responseList = _mapper.Map<List<GetAuthorInfoResponse>>(authorList);
        var response = new GetAuthorListResponse()
        {
            AuthorList = responseList
        };

        return response;
    }

    /// <inheritdoc />
    public async Task<GetAuthorSongInfoListResponse> GetAuthorSongInfoListAsync(Guid authorId)
    {
        using var log = new MethodLog(authorId);

        var songInfoList = await _authorManager.GetAuthorSongInfoListAsync(authorId);

        var responseList = _mapper.Map<List<GetAuthorSongInfoResponse>>(songInfoList);
        var response = new GetAuthorSongInfoListResponse()
        {
            SongInfoList = responseList
        };

        log.ReturnsValue(response);
        return response;
    }

    /// <inheritdoc />
    public async Task<GetSubscriberCountResponse> GetSubscriberCountAsync(Guid authorId)
    {
        using var log = new MethodLog(authorId);

        var count = await _authorRepository.GetSubcriberCountAsync(authorId);

        var response = new GetSubscriberCountResponse()
        {
            Count = count
        };

        return response;
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Guid authorId, UpdateAuthorRequest request)
    {
        using var log = new MethodLog(request);

        var author = _mapper.Map<AuthorDal>(request);
        await _authorManager.UpdateAsync(authorId, author);
    }

    /// <inheritdoc />
    public async Task<GetAuthorTotalAuditionCountResponse> GetAuthorTotalAuditionCountAsync(Guid authorId)
    {
        using var log = new MethodLog(authorId);

        var auditionCount = await _authorManager.GetAuthorTotalAuditionCountAsync(authorId);

        var response = new GetAuthorTotalAuditionCountResponse()
        {
            AuditionCount = auditionCount
        };

        return response;
    }
}