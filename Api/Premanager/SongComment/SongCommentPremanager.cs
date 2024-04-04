using Api.Controllers.SongComment.Response;
using AutoMapper;
using Dal.Author.Repository;
using Dal.BaseUser.Repository;
using Logic.SongComment;
using MainLib.Logging;

namespace Api.Premanager.SongComment;

/// <inheritdoc />
public class SongCommentPremanager : ISongCommentPremanager
{
    private readonly ISongCommentManager _songCommentManager;
    private readonly IMapper _mapper;

    private readonly IUserRepository _userRepository;
    private readonly IAuthorRepository _authorRepository;

    /// <summary>
    /// 
    /// </summary>
    public SongCommentPremanager(
        ISongCommentManager songCommentManager,
        IMapper mapper,
        IAuthorRepository authorRepository,
        IUserRepository userRepository)
    {
        _songCommentManager = songCommentManager;
        _mapper = mapper;
        _authorRepository = authorRepository;
        _userRepository = userRepository;
    }

    /// <inheritdoc />
    public async Task<GetSongCommentListResponse> GetSongCommentListAsync(Guid songId)
    {
        using var log = new MethodLog(songId);
        var commentList = await _songCommentManager.GetCommentListAsync(songId);

        var responseCommentList = _mapper.Map<List<GetSongCommentResponse>>(commentList);

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
        
        // for (var i = 0; i < responseCommentList.Count; i++)
        // {
        //     var responseComment = responseCommentList[i];
        //
        //     try
        //     {
        //         var author = await _authorRepository.GetAsync(responseComment.AuthorId);
        //         responseComment = responseComment with
        //         {
        //             AuthorDisplayedName = author.Name,
        //             IsSongAuthor = true
        //         };
        //     }
        //     catch (EntityNotFoundException<AuthorDal>)
        //     {
        //         var user = await _commonUserRepository.GetAsync(responseComment.AuthorId);
        //         responseComment = responseComment with
        //         {
        //             AuthorDisplayedName = user.UserName,
        //             IsSongAuthor = false
        //         };
        //     }
        //
        //     responseCommentList[i] = responseComment;
        // }

        var response = new GetSongCommentListResponse()
        {
            CommentList = responseCommentList
        };

        log.ReturnsValue(response);
        return response;
    }
}