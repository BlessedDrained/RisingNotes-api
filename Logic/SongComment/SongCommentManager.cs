using Dal.BaseUser.Repository;
using Dal.Comment;
using Dal.Comment.Repository;
using Dal.Song.Repository;
using MainLib.Logging;

namespace Logic.SongComment;

/// <inheritdoc />
public class SongCommentManager : ISongCommentManager
{
    private readonly IUserRepository _userRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly ISongRepository _songRepository;

    public SongCommentManager(
        IUserRepository userRepository,
        ICommentRepository commentRepository,
        ISongRepository songRepository)
    {
        _userRepository = userRepository;
        _commentRepository = commentRepository;
        _songRepository = songRepository;

    }

    /// <inheritdoc />
    public async Task<Guid> AddCommentAsync(Guid songId, Guid userId, string commentText)
    {
        using var log = new MethodLog(songId, userId, commentText);
        await _userRepository.GetAsync(userId);

        var commentModel = new CommentDal()
        {
            CreatorId = userId,
            Text = commentText,
            SongId = songId
        };

        var id = await _commentRepository.InsertAsync(commentModel);

        log.ReturnsValue(id);
        return id;
    }

    /// <inheritdoc />
    public async Task<List<CommentDal>> GetCommentListAsync(Guid songId)
    {
        using var log = new MethodLog(songId);

        await _songRepository.GetAsync(songId);

        var commentList = await _commentRepository.GetSongCommentListAsync(songId);

        log.ReturnsValue(commentList);
        return commentList;
    }

    /// <inheritdoc />
    public async Task EditCommentAsync(Guid commentId, string newText)
    {
        using var log = new MethodLog(commentId, newText);

        var commentModel = await _commentRepository.GetAsync(commentId);

        commentModel.Text = newText;

        await _commentRepository.UpdateAsync(commentModel);
    }

    /// <inheritdoc />
    public Task RemoveCommentAsync(Guid commentId)
    {
        using var log = new MethodLog(commentId);
        return _commentRepository.DeleteAsync(commentId);
    }
}