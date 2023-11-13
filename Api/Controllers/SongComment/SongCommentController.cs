using Api.Controllers.SongComment.Request;
using Api.Controllers.SongComment.Response;
using Api.Premanager.SongComment;
using Logic.SongComment;
using MainLib.Api.Auth.Constant;
using MainLib.Api.Controller;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.SongComment;

/// <inheritdoc />
[Route("song")]
public class SongCommentController : PublicController
{
    private readonly ISongCommentManager _songCommentManager;
    private readonly ISongCommentPremanager _songCommentPremanager;

    /// <inheritdoc />
    public SongCommentController(
        ISongCommentManager songCommentManager,
        ISongCommentPremanager songCommentPremanager)
    {
        _songCommentManager = songCommentManager;
        _songCommentPremanager = songCommentPremanager;
    }

    /// <summary>
    /// Добавить комментарий
    /// </summary>
    [HttpPost("{songId:guid}/comment")]
    [ProducesResponseType(201)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> AddCommentAsync(
        [FromRoute] Guid songId,
        [FromBody] AddCommentRequest request)
    {
        var userId = Guid.Parse(User.Identity!.Name!);
        await _songCommentManager.AddCommentAsync(songId, userId, request.Text);

        return Created(string.Empty, null);
    }

    /// <summary>
    /// Получить список комментариев к песне
    /// </summary>
    [HttpGet("{songId:guid}/comment/list")]
    [ProducesResponseType(typeof(GetSongCommentListResponse), 200)]
    public async Task<IActionResult> GetCommentListAsync([FromRoute] Guid songId)
    {
        var commentList = await _songCommentPremanager.GetSongCommentListAsync(songId);

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
        await _songCommentManager.EditCommentAsync(commentId, request.NewText);

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
        await _songCommentManager.RemoveCommentAsync(commentId);

        return NoContent();
    }
}