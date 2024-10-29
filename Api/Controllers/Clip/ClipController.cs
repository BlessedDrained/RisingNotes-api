using Api.Controllers.Clip.Dto.Request;
using Api.Controllers.Clip.Dto.Response;
using Api.Controllers.File.Dto.Request;
using Api.Premanager.Clip;
using Dal.MusicClip;
using Logic.MusicClip;
using MainLib.Api.Auth.Constant;
using MainLib.Api.Controller;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RisingNotesLib.Constant;
using RisingNotesLib.Helper;

namespace Api.Controllers.Clip;

/// <summary>
/// Контроллер для <see cref="ClipDal"/>
/// </summary>
[Route("music-clip")]
public class ClipController : PublicController
{
    private readonly IClipPremanager _clipPremanager;
    private readonly IClipManager _clipManager;

    public ClipController(
        IClipPremanager clipPremanager, 
        IClipManager clipManager)
    {
        _clipPremanager = clipPremanager;
        _clipManager = clipManager;
    }
    
    /// <summary>
    /// Загрузить клип
    /// </summary>
    [HttpPost]
    [ProducesResponseType(201)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAtLeastAuthor)]
    public async Task<IActionResult> UploadAsync([FromForm] UploadClipRequest request)
    {
        var claim = User.Claims.First(x => x.Type == ClaimTypeConstants.AuthorId);
        var authorId = Guid.Parse(claim.Value);
        var response = await _clipPremanager.UploadAsync(request, authorId);
        
        return CreatedAtAction("GetInfo", new {clipId = response.Id}, response);
    }

    /// <summary>
    /// Обновить превью клипа
    /// </summary>
    [HttpPatch("{clipId:guid}/preview")]
    [ProducesResponseType(204)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAtLeastAuthor)]
    public async Task<IActionResult> UpdatePreviewAsync([FromRoute] Guid clipId, [FromForm] UploadFileRequest request)
    {
        var claim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypeConstants.AuthorId);
        if (claim == null)
        {
            return Unauthorized();
        }

        var authorId = Guid.Parse(claim.Value);
        await _clipManager.UpdatePreviewAsync(authorId, clipId, request.File);

        return NoContent();
    }

    /// <summary>
    /// Начать операцию по загрузке обновленного файла с клипом
    /// </summary>
    [HttpPost("{clipId:guid}/file/start-upload")]
    [ProducesResponseType(typeof(StartClipUpdateResponse), 200)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAtLeastAuthor)]
    public async Task<IActionResult> StartClipUpdateAsync([FromRoute] Guid clipId)
    {
        var claim = User.Claims.First(x => x.Type == ClaimTypeConstants.AuthorId);
        var authorId = Guid.Parse(claim.Value);
        var uploadId = await _clipManager.StartClipFileUpdateAsync(authorId, clipId);

        return Ok(new StartClipUpdateResponse()
        {
            UploadId = uploadId
        });
    }

    /// <summary>
    /// Загрузить часть клипа
    /// </summary>
    [HttpPost("{clipId:guid}/file/upload-part")]
    [ProducesResponseType(typeof(StartClipUpdateResponse), 200)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAtLeastAuthor)]
    public async Task<IActionResult> UploadClipFilePartAsync([FromRoute] Guid clipId, [FromForm] UploadClipFilePartRequest request)
    {
        var authorId = Guid.Parse(User.Claims.First(x => x.Type == ClaimTypeConstants.AuthorId).Value);
        await _clipManager.UpdateClipFilePartAsync(request.UploadId, clipId, authorId, request.File, request.PartNumber, request.IsLastPart);

        return NoContent();
    }

    /// <summary>
    /// Получить информацию о клипе
    /// </summary>
    [HttpGet("{clipId:guid}")]
    [ProducesResponseType(typeof(GetClipInfoResponse), 200)]
    public async Task<IActionResult> GetInfoAsync([FromRoute] Guid clipId)
    {
        var response = await _clipPremanager.GetInfoAsync(clipId);
        
        return Ok(response);
    }
    
    /// <summary>
    /// Получить файл превью
    /// </summary>
    [HttpGet("{clipId:guid}/preview")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetPreviewAsync([FromRoute] Guid clipId)
    {
        var preview = await _clipManager.GetPreviewAsync(clipId);
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
        var file = await _clipManager.GetFileAsync(clipId);
        var contentType = ContentTypeHelper.GetContentTypeByFileExtension(file.Extension);

        return File(file.Content, contentType, enableRangeProcessing: true);
    }

    /// <summary>
    /// Служебный рест для получения информации о том, что клип можно получать по частям
    /// </summary>
    [HttpHead("{clipId:guid}/file")]
    [ProducesResponseType(200)]
    [ProducesResponseType(405)]
    public async Task<IActionResult> GetClipFileMetadataAsync([FromRoute] Guid clipId)
    {
        var fileMetadata = await _clipManager.GetClipMetadataAsync(clipId);
        
        // если файл достаточно большой, будем качать по частям
        if (fileMetadata.SizeBytes > 50 * 1024 * 1024 && fileMetadata.PartCount > 1)
        {
            Response.Headers.Add("accept-ranges", "bytes");
            Response.Headers.Add("content-length", fileMetadata.SizeBytes.ToString());
            return Ok();
        }

        // если файл маленький, т.е до 50 мб, то мы можем позволить себе скачать его за 1 раз
        return new StatusCodeResult(405);
    }

    /// <summary>
    /// Удалить клип
    /// </summary>
    [HttpDelete("{clipId:guid}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid clipId)
    {
        await _clipManager.DeleteAsync(clipId);

        return NoContent();
    }

    /// <summary>
    /// Получить список клипов по вайлдкарду названия
    /// </summary>
    [HttpGet("list")]
    [ProducesResponseType(typeof(GetClipInfoListResponse), 200)]
    public async Task<IActionResult> GetListAsync([FromQuery] string nameWildcard)
    {
        var response = await _clipPremanager.GetListAsync(nameWildcard);

        return Ok(response);
    }

    /// <summary>
    /// Получить список клипов автора
    /// </summary>
    [HttpGet("by-author/{authorId:guid}")]
    public async Task<IActionResult> GetAuthorClipListAsync([FromRoute] Guid authorId)
    {
        var response = await _clipPremanager.GetAuthorClipListAsync(authorId);

        return Ok(response);
    }

    /// <summary>
    /// Получить идентификатор клипа, который снят для данной песни
    /// </summary>
    [HttpGet("by-song/{songId:guid}")]
    public async Task<IActionResult> GetClipIdBySongIdAsync([FromRoute] Guid songId)
    {
        var response = await _clipPremanager.GetClipIdBySongIdAsync(songId);
        
        return Ok(response);
    }
}