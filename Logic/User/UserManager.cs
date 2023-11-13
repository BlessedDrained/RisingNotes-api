using Dal.BaseUser;
using Dal.BaseUser.Repository;
using Dal.File;
using Logic.File;
using MainLib.Logging;

namespace Logic.User;

public class UserManager : IUserManager
{
    private readonly IUserRepository _userRepository;
    private readonly IFileManager _fileManager;

    public UserManager(
        IFileManager fileManager,
        IUserRepository userRepository)
    {
        _fileManager = fileManager;
        _userRepository = userRepository;
    }

    /// <inheritdoc />
    public Task<Guid> CreateAsync(UserDal user)
    {
        return _userRepository.InsertAsync(user);
    }

    /// <inheritdoc />
    public async Task<FileDal> GetLogoAsync(Guid userId)
    {
        using var log = new MethodLog(userId);
        var user = await _userRepository.GetAsync(userId);
        var file = await _fileManager.DownloadAsync(user.LogoFileId.Value);

        log.ReturnsValue(file);
        return file;
    }
}