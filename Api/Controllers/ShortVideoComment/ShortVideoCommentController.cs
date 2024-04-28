using Api.Controllers.SongComment.Request;
using Api.Controllers.SongComment.Response;
using Api.Premanager.ShortVideoComment;
using Dal.ShortVideoComment;
using Logic.ShortVideoComment;
using MainLib.Api.Auth.Constant;
using MainLib.Api.Controller;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.ShortVideoComment;

/// <summary>
/// Контроллер для <see cref="ShortVideoCommentDal"/>
/// </summary>
[Route("short-video")]
public class ShortVideoCommentController : PublicController
{
    private readonly IShortVideoCommentManager _shortVideoCommentManager;
    private readonly IShortVideoCommentPremanager _shortVideoCommentPremanager;

    /// <inheritdoc />
    public ShortVideoCommentController(
        IShortVideoCommentManager shortVideoCommentManager,
        IShortVideoCommentPremanager shortVideoCommentPremanager)
    {
        _shortVideoCommentManager = shortVideoCommentManager;
        _shortVideoCommentPremanager = shortVideoCommentPremanager;
    }

    /// <summary>
    /// Добавить комментарий
    /// </summary>
    [HttpPost("{musicClipId:guid}/comment")]
    [ProducesResponseType(201)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> AddCommentAsync(
        [FromRoute] Guid musicClipId,
        [FromBody] AddCommentRequest request)
    {
        var userId = Guid.Parse(User.Identity!.Name!);
        await _shortVideoCommentManager.AddCommentAsync(musicClipId, userId, request.Text);

        return Created(string.Empty, null);
    }

    /// <summary>
    /// Получить список комментариев к песне
    /// </summary>
    [HttpGet("{musicClipId:guid}/comment/list")]
    [ProducesResponseType(typeof(GetSongCommentListResponse), 200)]
    public async Task<IActionResult> GetCommentListAsync([FromRoute] Guid musicClipId)
    {
        var commentList = await _shortVideoCommentPremanager.GetShortVideoCommentListAsync(musicClipId);

        return Ok(commentList);
    }

    /// <summary>
    /// Изменить комментарий
    /// </summary>
    [HttpPut("comment/{commentId:guid}")]
    [ProducesResponseType(204)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAtLeastUser)]
    public async Task<IActionResult> EditCommentAsync(
        [FromRoute] Guid commentId,
        [FromBody] EditCommentRequest request)
    {
        await _shortVideoCommentManager.EditCommentAsync(commentId, request.NewText);

        return NoContent();
    }

    /// <summary>
    /// Удалить комментарий
    /// </summary>
    [HttpDelete("comment/{commentId:guid}")]
    [ProducesResponseType(204)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAtLeastUser)]
    public async Task<IActionResult> RemoveCommentAsync(
        [FromRoute] Guid commentId)
    {
        await _shortVideoCommentManager.RemoveCommentAsync(commentId);

        return NoContent();
    }
}