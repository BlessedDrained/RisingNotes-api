using Api.Controllers.SongComment.Request;
using Api.Controllers.SongComment.Response;
using Api.Premanager.ClipComment;
using Dal.MusicClipComment;
using Logic.MusicClipComment;
using MainLib.Api.Auth.Constant;
using MainLib.Api.Controller;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.MusicClipComment;

/// <summary>
/// Контроллер для <see cref="ClipCommentDal"/>
/// </summary>
[Route("music-clip")]
public class MusicClipCommentController : PublicController
{
    private readonly IClipCommentManager _songCommentManager;
    private readonly IClipCommentPremanager _songCommentPremanager;

    /// <inheritdoc />
    public MusicClipCommentController(
        IClipCommentManager songCommentManager,
        IClipCommentPremanager songCommentPremanager)
    {
        _songCommentManager = songCommentManager;
        _songCommentPremanager = songCommentPremanager;
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
        await _songCommentManager.AddCommentAsync(musicClipId, userId, request.Text);

        return Created(string.Empty, null);
    }

    /// <summary>
    /// Получить список комментариев к песне
    /// </summary>
    [HttpGet("{musicClipId:guid}/comment/list")]
    [ProducesResponseType(typeof(GetSongCommentListResponse), 200)]
    public async Task<IActionResult> GetCommentListAsync([FromRoute] Guid musicClipId)
    {
        var commentList = await _songCommentPremanager.GetClipCommentListAsync(musicClipId);

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