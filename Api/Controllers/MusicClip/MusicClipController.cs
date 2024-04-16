using Api.Controllers.MusicClip.Dto.Request;
using Api.Controllers.MusicClip.Dto.Response;
using Api.Premanager.MusicClip;
using Dal.MusicClip;
using Logic.MusicClip;
using MainLib.Api.Auth.Constant;
using MainLib.Api.Controller;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RisingNotesLib.Constant;
using RisingNotesLib.Helper;

namespace Api.Controllers.MusicClip;

/// <summary>
/// Контроллер для <see cref="MusicClipDal"/>
/// </summary>
[Route("music-clip")]
public class MusicClipController : PublicController
{
    private readonly IMusicClipPremanager _musicClipPremanager;
    private readonly IMusicClipManager _musicClipManager;

    public MusicClipController(
        IMusicClipPremanager musicClipPremanager, 
        IMusicClipManager musicClipManager)
    {
        _musicClipPremanager = musicClipPremanager;
        _musicClipManager = musicClipManager;
    }
    
    /// <summary>
    /// Загрузить клип
    /// </summary>
    [HttpPost]
    [ProducesResponseType(201)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAtLeastAuthor)]
    public async Task<IActionResult> UploadAsync([FromForm] UploadClipRequest request)
    {
        var claim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypeConstants.AuthorId);
        if (claim == null)
        {
            return Unauthorized();
        }
        
        var authorId = Guid.Parse(claim.Value);
        var response = await _musicClipPremanager.UploadAsync(request, authorId);
        
        return CreatedAtAction("GetInfo", new {clipId = response.Id}, response);
    }

    /// <summary>
    /// Получить информацию о клипе
    /// </summary>
    [HttpGet("{clipId:guid}")]
    [ProducesResponseType(typeof(GetMusicClipInfoResponse), 200)]
    public async Task<IActionResult> GetInfoAsync([FromRoute] Guid clipId)
    {
        var response = await _musicClipPremanager.GetInfoAsync(clipId);
        
        return Ok(response);
    }
    
    /// <summary>
    /// Получить файл превью
    /// </summary>
    [HttpGet("{clipId:guid}/preview")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetPreviewAsync([FromRoute] Guid clipId)
    {
        var preview = await _musicClipManager.GetPreviewAsync(clipId);
        var contentType = ContentTypeHelper.GetContentTypeByFileExtension(preview.Extension);

        return File(preview.Content, contentType, enableRangeProcessing: true);
    }

    /// <summary>
    /// Получить файл с клипом
    /// </summary>
    [HttpGet("{clipId:guid}/file")]
    [ProducesResponseType(200)]
    [ProducesResponseType(206)]
    public async Task<IActionResult> GetFileAsync([FromRoute] Guid clipId)
    {
        var file = await _musicClipManager.GetFileAsync(clipId);
        var contentType = ContentTypeHelper.GetContentTypeByFileExtension(file.Extension);

        return File(file.Content, contentType);
    }

    /// <summary>
    /// Удалить клип
    /// </summary>
    [HttpDelete("{clipId:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid clipId)
    {
        await _musicClipManager.DeleteAsync(clipId);

        return NoContent();
    }
}