using Dal.BaseUser.Repository;
using Dal.Song.Repository;
using Dal.SongComment;
using Dal.SongComment.Repository;
using MainLib.Logging;

namespace Logic.SongComment;

/// <inheritdoc />
public class SongCommentManager : ISongCommentManager
{
    private readonly IUserRepository _userRepository;
    private readonly ISongCommentRepository _songCommentRepository;
    private readonly ISongRepository _songRepository;

    public SongCommentManager(
        IUserRepository userRepository,
        ISongCommentRepository songCommentRepository,
        ISongRepository songRepository)
    {
        _userRepository = userRepository;
        _songCommentRepository = songCommentRepository;
        _songRepository = songRepository;

    }

    /// <inheritdoc />
    public async Task<Guid> AddCommentAsync(Guid songId, Guid userId, string commentText)
    {
        using var log = new MethodLog(songId, userId, commentText);

        await using var transaction = await _userRepository.BeginTransactionOrExistingAsync();
        
        await _userRepository.GetAsync(userId);

        var commentModel = new SongCommentDal()
        {
            CreatorId = userId,
            Text = commentText,
            SongId = songId
        };

        var id = await _songCommentRepository.InsertAsync(commentModel);

        log.ReturnsValue(id);
        return id;
    }

    /// <inheritdoc />
    public async Task<List<SongCommentDal>> GetCommentListAsync(Guid songId)
    {
        using var log = new MethodLog(songId);

        await using var transaction = await _songRepository.BeginTransactionOrExistingAsync();

        await _songRepository.GetAsync(songId);

        var commentList = await _songCommentRepository.GetSongCommentListAsync(songId);

        log.ReturnsValue(commentList);
        return commentList;
    }

    /// <inheritdoc />
    public async Task EditCommentAsync(Guid commentId, string newText)
    {
        using var log = new MethodLog(commentId, newText);

        await using var transaction = await _songCommentRepository.BeginTransactionOrExistingAsync();
        
        var commentModel = await _songCommentRepository.GetAsync(commentId);

        commentModel.Text = newText;

        await _songCommentRepository.UpdateAsync(commentModel);
    }

    /// <inheritdoc />
    public Task RemoveCommentAsync(Guid commentId)
    {
        using var log = new MethodLog(commentId);
        return _songCommentRepository.DeleteAsync(commentId);
    }
}