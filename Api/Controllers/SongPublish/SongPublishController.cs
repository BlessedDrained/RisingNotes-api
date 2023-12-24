using Api.Controllers.SongPublish.Dto.Request;
using Api.Controllers.SongPublish.Dto.Response;
using Api.Premanager.SongPublish;
using Logic.Logo;
using Logic.SongPublish;
using MainLib.Api.Auth.Constant;
using MainLib.Api.Controller;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RisingNotesLib.Constant;
using RisingNotesLib.Exceptions;
using RisingNotesLib.Helper;

namespace Api.Controllers.SongPublish;

/// <summary>
/// Контроллер для работы с заявками на публикацию песен
/// </summary>
[Route("song/upload-request")]
public class SongPublishController : PublicController
{
    private readonly ISongPublishManager _songPublishManager;
    private readonly ISongPublishPremanager _songPublishPremanager;

    public SongPublishController(ISongPublishManager songPublishManager, ISongPublishPremanager songPublishPremanager)
    {
        _songPublishManager = songPublishManager;
        _songPublishPremanager = songPublishPremanager;
    }

    /// <summary>
    /// Создать заявку на публикацию песни
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(CreateSongPublishRequestResponse), 201)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAtLeastAuthor)]
    public async Task<IActionResult> CreatePublishRequestAsync([FromForm] CreateSongPublishRequestRequest request)
    {
        var userId = Guid.Parse(User.Identity!.Name!);
        var response = await _songPublishPremanager.CreateAsync(userId, request);

        return Created(string.Empty, response);
    }

    /// <summary>
    /// Ответить на заявку
    /// </summary>
    [HttpPatch("{requestId:guid}/admin")]
    [ProducesResponseType(204)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAdmin)]
    public async Task<IActionResult> ReplyRequestAsAdminAsync([FromRoute] Guid requestId, [FromBody] ReplyToRequestAsAdminRequest request)
    {
        await _songPublishManager.ReplyAsAdminAsync(requestId, request.Status, request.Comment);

        return NoContent();
    }

    /// <summary>
    /// Ответить на заявку
    /// </summary>
    [HttpPatch("{requestId:guid}/author")]
    [ProducesResponseType(204)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAtLeastAuthor)]
    public async Task<IActionResult> ReplyRequestAsAuthorAsync([FromRoute] Guid requestId, [FromForm] ReplyToRequestAsUserRequest request)
    {
        await _songPublishPremanager.ReplyRequestAsUserAsync(requestId, request);

        return NoContent();
    }

    /// <summary>
    /// Получить список краткой информации о заявках
    /// </summary>
    [HttpGet("list")]
    [ProducesResponseType(typeof(GetPublishRequestShortInfoListResponse), 200)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAtLeastAuthor)]
    public async Task<IActionResult> GetUserRequestListAsync([FromQuery] GetPublishRequestListRequest request)
    {
        var claim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypeConstants.AuthorId);
        if (claim == null)
        {
            return Unauthorized();
        }
        
        var response = await _songPublishPremanager.GetListAsync(request, Guid.Parse(claim.Value));

        return Ok(response);
    }

    /// <summary>
    /// Получить список заявок для ревью
    /// </summary>
    /// <returns></returns>
    [HttpGet("list/for-review")]
    [ProducesResponseType(typeof(GetPublishRequestShortInfoListResponse), 200)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAdmin)]
    public async Task<IActionResult> GetToReviewRequestListAsync()
    {
        var response = await _songPublishPremanager.GetForReviewListAsync();

        return Ok(response);
    }

    /// <summary>
    /// Получить подробную информацию о заявке
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetPublishRequestInfoResponse), 200)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAtLeastAuthor)]
    public async Task<IActionResult> GetFullInfoAsync(Guid id)
    {
        var response = await _songPublishPremanager.GetFullInfoAsync(id);

        return Ok(response);
    }

    /// <summary>
    /// Получить файл с треком из заявки
    /// </summary>
    [HttpGet("{requestId:guid}/file")]
    [ProducesResponseType(200)]
    [ProducesResponseType(206)]
    public async Task<IActionResult> GetFileAsync([FromRoute] Guid requestId, [FromServices] ISongPublishManager songPublishManager)
    {
        var file = await _songPublishManager.GetSongFileAsync(requestId);
        var contentType = ContentTypeHelper.GetContentTypeByFileExtension(file.Extension);

        return File(file.Content, contentType, enableRangeProcessing: true);
    }

    /// <summary>
    /// Получить лого трека из заявки
    /// </summary>
    [HttpGet("{requestId:guid}/logo")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetLogoAsync(
        [FromRoute] Guid requestId,
        [FromQuery] int? width,
        [FromQuery] int? height,
        [FromServices] ILogoResizeService logoResizeService)
    {
        if (!width.HasValue && !height.HasValue)
        {
            throw new InvalidImageSizeException();
        }

        var logo = await _songPublishManager.GetLogoAsync(requestId);
        var resized = await logoResizeService.ResizeAsync(logo, width, height);
        var contentType = ContentTypeHelper.GetContentTypeByFileExtension(logo.Extension);

        return File(resized, contentType);
    }
}