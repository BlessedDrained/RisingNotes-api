using Api.Controllers.ExcludedTrack.Dto;
using Api.Controllers.File.Dto.Request;
using Api.Controllers.Song.Dto.Response;
using Api.Controllers.Subscription.Dto.Response;
using AutoMapper;
using Dal.BaseUser.Repository;
using Dal.File;
using Logic.User;
using MainLib.Logging;

namespace Api.Premanager.User;

/// <inheritdoc />
public class UserPremanager : IUserPremanager
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IUserManager _userManager;

    /// <summary>
    /// 
    /// </summary>
    public UserPremanager(IUserRepository userRepository, IMapper mapper, IUserManager userManager)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _userManager = userManager;
    }

    public Task<List<GetWithAuthorSongInfoResponse>> GetFavoriteSongInfoListAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Получить список подписок
    /// </summary>
    /// <returns></returns>
    public async Task<GetSubscriptionListResponse> GetSubscriptionListAsync(Guid userId)
    {
        var subscriptionAuthorIdListAsync = await _userRepository.GetSubscriptionListAsync(userId);
        var responseList = subscriptionAuthorIdListAsync
            .Select(x => new GetSubscriptionInfoResponse()
            {
                AuthorId = x
            })
            .ToList();

        var response = new GetSubscriptionListResponse()
        {
            SubscriptionList = responseList
        };

        return response;
    }

    /// <inheritdoc />
    public async Task UpdateLogoAsync(Guid userId, UploadFileRequest request)
    {
        using var log = new MethodLog(userId, request);

        var file = _mapper.Map<FileDal>(request);
        await _userManager.UpdateLogoAsync(userId, file);
    }

    /// <inheritdoc />
    public async Task<GetExcludedTrackListResponse> GetExcludedTrackListAsync(Guid userId)
    {
        using var log = new MethodLog(userId);

        var excludedTrackList = await _userRepository.GetExcludedTrackListAsync(userId);

        var responseList = _mapper.Map<List<GetExcludedTrackInfoResponse>>(excludedTrackList);

        var response = new GetExcludedTrackListResponse()
        {
            ExcludedTrackList = responseList
        };
        return response;
    }
}