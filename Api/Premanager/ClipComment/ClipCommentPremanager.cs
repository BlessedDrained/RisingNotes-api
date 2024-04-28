using Api.Controllers.MusicClipComment.Dto.Response;
using AutoMapper;
using Dal.BaseUser.Repository;
using Logic.MusicClip;
using Logic.MusicClipComment;
using MainLib.Logging;

namespace Api.Premanager.ClipComment;

/// <inheritdoc />
public class ClipCommentPremanager : IClipCommentPremanager
{
    private readonly IClipCommentManager _clipCommentManager;
    private readonly IClipManager _clipManager;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public ClipCommentPremanager(
        IClipCommentManager clipCommentManager,
        IClipManager clipManager,
        IMapper mapper,
        IUserRepository userRepository)
    {
        _clipCommentManager = clipCommentManager;
        _clipManager = clipManager;
        _mapper = mapper;
        _userRepository = userRepository;
    }
    
    /// <inheritdoc />
    public async Task<GetClipCommentListResponse> GetClipCommentListAsync(Guid musicClipId)
    {
        using var log = new MethodLog(musicClipId);
        var commentList = await _clipCommentManager.GetCommentListAsync(musicClipId);

        var responseCommentList = _mapper.Map<List<GetClipCommentResponse>>(commentList);

        for (var i = 0; i < responseCommentList.Count; i++)
        {
            var responseComment = responseCommentList[i];
            var commentAuthor = await _userRepository.GetAsync(responseComment.AuthorId);
            responseComment = responseComment with
            {
                AuthorDisplayedName = commentAuthor.UserName
            };

            responseCommentList[i] = responseComment;
        }

        var response = new GetClipCommentListResponse()
        {
            CommentList = responseCommentList
        };

        log.ReturnsValue(response);
        return response;
    }
}