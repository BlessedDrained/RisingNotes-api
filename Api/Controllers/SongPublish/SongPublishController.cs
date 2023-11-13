using System.Security.Claims;
using Api.Controllers.SongPublish.Dto.Request;
using Api.Controllers.SongPublish.Dto.Response;
using Api.Premanager.SongPublish;
using Logic.SongPublish;
using MainLib.Api.Auth.Constant;
using MainLib.Api.Controller;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> GetRequestListAsync([FromQuery] GetPublishRequestListRequest request)
    {
        var roleClaim = User.Claims.First(x => x.Type == ClaimTypes.Role);

        if (roleClaim.Value == RoleConstants.User)
        {
            return Unauthorized();
        }

        var isAdmin = roleClaim.Value == RoleConstants.Admin;

        var response = await _songPublishPremanager.GetListAsync(request, isAdmin);

        return Ok(response);
    }
}