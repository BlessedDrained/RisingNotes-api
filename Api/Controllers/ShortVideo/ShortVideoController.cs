using Api.Controllers.ShortVideo.Dto.Request;
using Api.Controllers.ShortVideo.Dto.Response;
using Api.Premanager.ShortVideo;
using Dal.ShortVideo;
using Logic.ShortVideo;
using MainLib.Api.Auth.Constant;
using MainLib.Api.Controller;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RisingNotesLib.Constant;
using RisingNotesLib.Helper;

namespace Api.Controllers.ShortVideo;

/// <summary>
/// Контроллер для <see cref="ShortVideoDal"/>
/// </summary>
[Route("short-video")]
public class ShortVideoController : PublicController
{
    private readonly IShortVideoPremanager _shortVideoPremanager;
    private readonly IShortVideoManager _shortVideoManager;

    public ShortVideoController(
        IShortVideoPremanager shortVideoPremanager,
        IShortVideoManager shortVideoManager)
    {
        _shortVideoPremanager = shortVideoPremanager;
        _shortVideoManager = shortVideoManager;
    }

    /// <summary>
    /// Загрузить клип
    /// </summary>
    [HttpPost]
    [ProducesResponseType(201)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAtLeastAuthor)]
    public async Task<IActionResult> UploadAsync([FromForm] UploadShortVideoRequest request)
    {
        var claim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypeConstants.AuthorId);
        if (claim == null)
        {
            return Unauthorized();
        }

        var authorId = Guid.Parse(claim.Value);
        var response = await _shortVideoPremanager.UploadAsync(request, authorId);

        return CreatedAtAction("GetInfo", new {shortVideoId = response.Id}, response);
    }

    /// <summary>
    /// Получить информацию о клипе
    /// </summary>
    [HttpGet("{shortVideoId:guid}")]
    [ProducesResponseType(typeof(GetShortVideoInfoResponse), 200)]
    public async Task<IActionResult> GetInfoAsync([FromRoute] Guid shortVideoId)
    {
        var response = await _shortVideoPremanager.GetInfoAsync(shortVideoId);

        return Ok(response);
    }

    /// <summary>
    /// Получить файл превью
    /// </summary>
    [HttpGet("{shortVideoId:guid}/preview")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetPreviewAsync([FromRoute] Guid shortVideoId)
    {
        var preview = await _shortVideoManager.GetPreviewAsync(shortVideoId);
        var contentType = ContentTypeHelper.GetContentTypeByFileExtension(preview.Extension);

        return File(preview.Content, contentType, enableRangeProcessing: true);
    }

    /// <summary>
    /// Получить файл с клипом
    /// </summary>
    [HttpGet("{shortVideoId:guid}/file")]
    [ProducesResponseType(200)]
    [ProducesResponseType(206)]
    public async Task<IActionResult> GetFileAsync([FromRoute] Guid shortVideoId)
    {
        var file = await _shortVideoManager.GetFileAsync(shortVideoId);
        var contentType = ContentTypeHelper.GetContentTypeByFileExtension(file.Extension);

        return File(file.Content, contentType, enableRangeProcessing: true);
    }

    /// <summary>
    /// Удалить клип
    /// </summary>
    [HttpDelete("{shortVideoId:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid shortVideoId)
    {
        await _shortVideoManager.DeleteAsync(shortVideoId);

        return NoContent();
    }
    
    /// <summary>
    /// Получить список клипов по вайлдкарду названия
    /// </summary>
    [HttpGet("list")]
    [ProducesResponseType(typeof(GetShortVideoInfoListResponse), 200)]
    public async Task<IActionResult> GetShortVideoInfoListAsync([FromQuery] string nameWildcard)
    {
        var response = await _shortVideoPremanager.GetListAsync(nameWildcard);

        return Ok(response);
    }

    /// <summary>
    /// Получить список клипов автора
    /// </summary>
    [HttpGet("by-author/{authorId:guid}")]
    [ProducesResponseType(typeof(GetShortVideoInfoListResponse), 200)]
    public async Task<IActionResult> GetAuthorShortVideoListAsync([FromRoute] Guid authorId)
    {
        var response = await _shortVideoPremanager.GetAuthorClipListAsync(authorId);

        return Ok(response);
    }
}