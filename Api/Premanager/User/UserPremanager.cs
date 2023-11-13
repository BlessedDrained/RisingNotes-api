using Api.Controllers.Song.Dto.Response;
using Dal.BaseUser.Repository;

namespace Api.Premanager.User;

/// <inheritdoc />
public class UserPremanager : IUserPremanager
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// 
    /// </summary>
    public UserPremanager(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<List<GetSongInfoResponse>> GetFavoriteSongInfoListAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}