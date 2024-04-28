using Dal.BaseUser.Repository;
using Dal.MusicClipComment;
using Dal.ShortVideo.Repository;
using Dal.ShortVideoComment;
using Dal.ShortVideoComment.Repository;
using MainLib.Logging;

namespace Logic.ShortVideoComment;

/// <inheritdoc />
public class ShortVideoCommentManager : IShortVideoCommentManager
{
    private readonly IShortVideoCommentRepository _shortVideoCommentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IShortVideoRepository _shortVideoRepository;

    public ShortVideoCommentManager(
        IShortVideoCommentRepository shortVideoCommentRepository, 
        IUserRepository userRepository,
        IShortVideoRepository shortVideoRepository)
    {
        _shortVideoCommentRepository = shortVideoCommentRepository;
        _userRepository = userRepository;
        _shortVideoRepository = shortVideoRepository;
    }
    
    /// <inheritdoc />
    public async Task<Guid> AddCommentAsync(Guid songId, Guid userId, string commentText)
    {
        using var log = new MethodLog(songId, userId, commentText);
        
        await _userRepository.GetAsync(userId);

        var commentModel = new ShortVideoCommentDal()
        {
            CreatorId = userId,
            Text = commentText,
            ShortVideoId = songId
        };

        var id = await _shortVideoCommentRepository.InsertAsync(commentModel);

        log.ReturnsValue(id);
        return id;
    }

    /// <inheritdoc />
    public async Task<List<ShortVideoCommentDal>> GetCommentListAsync(Guid songId)
    {
        using var log = new MethodLog(songId);

        // await using var transaction = await _songRepository.BeginTransactionOrExistingAsync();

        await _shortVideoRepository.GetAsync(songId);

        var commentList = await _shortVideoCommentRepository.GetShortVideoCommentListAsync(songId);

        log.ReturnsValue(commentList);
        return commentList;
    }

    /// <inheritdoc />
    public async Task EditCommentAsync(Guid commentId, string newText)
    {
        using var log = new MethodLog(commentId, newText);

        // await using var transaction = await _songCommentRepository.BeginTransactionOrExistingAsync();
        
        var commentModel = await _shortVideoCommentRepository.GetAsync(commentId);

        commentModel.Text = newText;

        await _shortVideoCommentRepository.UpdateAsync(commentModel);
    }

    /// <inheritdoc />
    public Task RemoveCommentAsync(Guid commentId)
    {
        using var log = new MethodLog(commentId);
        return _shortVideoCommentRepository.DeleteAsync(commentId);
    }
}