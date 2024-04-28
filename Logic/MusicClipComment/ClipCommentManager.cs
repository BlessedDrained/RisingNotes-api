using Dal.BaseUser.Repository;
using Dal.MusicClip.Repository;
using Dal.MusicClipComment;
using Dal.MusicClipComment.Repository;
using MainLib.Logging;

namespace Logic.MusicClipComment;

/// <inheritdoc />
public class ClipCommentManager : IClipCommentManager
{
    private readonly IClipCommentRepository _clipCommentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IClipRepository _clipRepository;

    public ClipCommentManager(
        IClipCommentRepository clipCommentRepository, 
        IUserRepository userRepository,
        IClipRepository clipRepository)
    {
        _clipCommentRepository = clipCommentRepository;
        _userRepository = userRepository;
        _clipRepository = clipRepository;
    }

    /// <inheritdoc />
    public async Task<Guid> AddCommentAsync(Guid songId, Guid userId, string commentText)
    {
        using var log = new MethodLog(songId, userId, commentText);

        // await using var transaction = await _userRepository.BeginTransactionOrExistingAsync();
        
        await _userRepository.GetAsync(userId);

        var commentModel = new ClipCommentDal()
        {
            CreatorId = userId,
            Text = commentText,
            SongId = songId
        };

        var id = await _clipCommentRepository.InsertAsync(commentModel);

        log.ReturnsValue(id);
        return id;
    }

    /// <inheritdoc />
    public async Task<List<ClipCommentDal>> GetCommentListAsync(Guid songId)
    {
        using var log = new MethodLog(songId);

        // await using var transaction = await _songRepository.BeginTransactionOrExistingAsync();

        await _clipRepository.GetAsync(songId);

        var commentList = await _clipCommentRepository.GetClipCommentListAsync(songId);

        log.ReturnsValue(commentList);
        return commentList;
    }

    /// <inheritdoc />
    public async Task EditCommentAsync(Guid commentId, string newText)
    {
        using var log = new MethodLog(commentId, newText);

        // await using var transaction = await _songCommentRepository.BeginTransactionOrExistingAsync();
        
        var commentModel = await _clipCommentRepository.GetAsync(commentId);

        commentModel.Text = newText;

        await _clipCommentRepository.UpdateAsync(commentModel);
    }

    /// <inheritdoc />
    public Task RemoveCommentAsync(Guid commentId)
    {
        using var log = new MethodLog(commentId);
        return _clipCommentRepository.DeleteAsync(commentId);
    }
}