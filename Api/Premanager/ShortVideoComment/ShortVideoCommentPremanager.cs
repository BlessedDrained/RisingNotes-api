using Api.Controllers.ShortVideoComment.Dto.Response;
using AutoMapper;
using Dal.BaseUser.Repository;
using Logic.ShortVideo;
using Logic.ShortVideoComment;
using MainLib.Logging;

namespace Api.Premanager.ShortVideoComment;

/// <inheritdoc />
public class ShortVideoCommentPremanager : IShortVideoCommentPremanager
{
    private readonly IShortVideoCommentManager _clipCommentManager;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public ShortVideoCommentPremanager(
        IShortVideoCommentManager clipCommentManager,
        IMapper mapper,
        IUserRepository userRepository)
    {
        _clipCommentManager = clipCommentManager;
        _mapper = mapper;
        _userRepository = userRepository;
    }
    
    /// <inheritdoc />
    public async Task<GetShortVideoCommentListResponse> GetShortVideoCommentListAsync(Guid musicClipId)
    {
        using var log = new MethodLog(musicClipId);
        var commentList = await _clipCommentManager.GetCommentListAsync(musicClipId);

        var responseCommentList = _mapper.Map<List<GetShortVideoCommentResponse>>(commentList);

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

        var response = new GetShortVideoCommentListResponse()
        {
            CommentList = responseCommentList
        };

        log.ReturnsValue(response);
        return response;
    }
}