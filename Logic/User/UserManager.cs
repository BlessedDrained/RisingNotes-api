using Dal.Author.Repository;
using Dal.BaseUser;
using Dal.BaseUser.Repository;
using Dal.File;
using Logic.File;
using MainLib.Logging;

namespace Logic.User;

public class UserManager : IUserManager
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IFileManager _fileManager;

    public UserManager(
        IFileManager fileManager,
        IUserRepository userRepository,
        IAuthorRepository authorRepository)
    {
        _fileManager = fileManager;
        _userRepository = userRepository;
        _authorRepository = authorRepository;
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

    /// <summary>
    /// Подписаться на автора
    /// </summary>
    public async Task SubscribeAsync(Guid userId, Guid authorId)
    {
        using var log = new MethodLog(userId, authorId);

        var user = await _userRepository.GetWithSubscriptionListAsync(userId);
        if (user.SubscriptionList.Exists(x => x.Id == authorId))
        {
            throw new Exception($"User with id={userId} already has subscription to author with id={authorId}");
        }

        var author = await _authorRepository.GetAsync(authorId);

        user.SubscriptionList.Add(author);
        await _userRepository.UpdateAsync(user);
    }

    /// <inheritdoc />
    public async Task UnsubscribeAsync(Guid userId, Guid authorId)
    {
        using var log = new MethodLog(userId, authorId);

        var user = await _userRepository.GetWithSubscriptionListAsync(userId);
        if (!user.SubscriptionList.Exists(x => x.Id == authorId))
        {
            throw new Exception($"User with id={userId} does not have subscription to author with id={authorId}");
        }

        var author = await _authorRepository.GetAsync(authorId);

        user.SubscriptionList.Remove(author);
        await _userRepository.UpdateAsync(user);
    }

    public async Task UpdateLogoAsync(Guid userId, FileDal file)
    {
        using var log = new MethodLog(userId, file);
        
        var user = await _userRepository.GetAsync(userId);

        // if (user.LogoFileId.HasValue)
        // {
        //     await _fileManager.DeleteAsync(user.LogoFileId.Value);
        // }

        var fileId = await _fileManager.UploadAsync(file);

        user.LogoFile = file;
        user.LogoFileId = fileId;

        await _userRepository.UpdateAsync(user);
    }
}