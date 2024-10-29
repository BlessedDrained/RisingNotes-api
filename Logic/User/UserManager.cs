using Dal.Author.Repository;
using Dal.BaseUser;
using Dal.BaseUser.Repository;
using Dal.File;
using Logic.File;
using MainLib.Logging;
using RisingNotesLib.Exceptions;

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
        using var log = new MethodLog(user);
        
        return _userRepository.InsertAsync(user);
    }

    /// <inheritdoc />
    public async Task<FileDal> GetLogoAsync(Guid userId)
    {
        using var log = new MethodLog(userId);
        
        // await using var transaction = await _userRepository.BeginTransactionOrExistingAsync();
        
        var user = await _userRepository.GetAsync(userId);
        if (!user.LogoFileId.HasValue)
        {
            throw new UserHasNoLogoException(userId);
        }

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
        
        // await using var transaction = await _userRepository.BeginTransactionOrExistingAsync();

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

        // await using var transaction = await _userRepository.BeginTransactionOrExistingAsync();
        
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

        // await using var transaction = await _userRepository.BeginTransactionOrExistingAsync();
        
        var user = await _userRepository.GetAsync(userId);

        if (user.LogoFileId.HasValue)
        {
            await _fileManager.DeleteAsync(user.LogoFileId.Value);
        }

        await _fileManager.UploadSingleAsync(file);

        user.LogoFile = file;
        user.LogoFileId = file.Id;

        await _userRepository.UpdateAsync(user);
    }

    /// <inheritdoc />
    public async Task UpdateUserNameAsync(Guid userId, string userName)
    {
        using var log = new MethodLog(userId, userName);

        // await using var transaction = await _userRepository.BeginTransactionOrExistingAsync();
        
        var nameAlreadyUsed = await _userRepository.ExistsAsync(x => x.UserName == userName);
        if (nameAlreadyUsed)
        {
            throw new UserNameIsAlreadyTakenException(userName);
        }

        var user = await _userRepository.GetAsync(userId);
        user.UserName = userName;
        await _userRepository.UpdateAsync(user);
    }
}