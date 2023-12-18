using Api.Controllers.ExcludedTrack.Dto;
using Api.Premanager.User;
using Logic.Song;
using MainLib.Api.Auth.Constant;
using MainLib.Api.Controller;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.ExcludedTrack;

/// <summary>
/// Контроллер для исключенных треков
/// </summary>
[Route("excluded-track")]
public class ExcludedTrackController : PublicController
{
    private readonly ISongManager _songManager;
    private readonly IUserPremanager _userPremanager;

    /// <inheritdoc />
    public ExcludedTrackController(ISongManager songManager, IUserPremanager userPremanager)
    {
        _songManager = songManager;
        _userPremanager = userPremanager;
    }

    /// <summary>
    /// Добавить трек в исключенные
    /// </summary>
    [HttpPost("{songId:guid}")]
    [ProducesResponseType(201)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAtLeastUser)]
    public async Task<IActionResult> ExcludeAsync(
        [FromRoute] Guid songId)
    {
        var userId = Guid.Parse(User.Identity!.Name!);
        await _songManager.ExcludeAsync(userId, songId);

        return CreatedAtAction("GetList", null, null);
    }

    /// <summary>
    /// Убрать из исключенных
    /// </summary>
    [HttpDelete("{songId:guid}")]
    [ProducesResponseType(204)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAtLeastUser)]
    public async Task<IActionResult> RemoveFromExcludedAsync(
        [FromRoute] Guid songId)
    {
        var userId = Guid.Parse(User.Identity!.Name!);
        await _songManager.RemoveFromExcludedAsync(userId, songId);

        return NoContent();
    }

    /// <summary>
    /// Получить список исключенных треков
    /// </summary>
    /// <returns></returns>
    [HttpGet("list")]
    [ProducesResponseType(typeof(GetExcludedTrackListResponse), 200)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAtLeastUser)]
    public async Task<IActionResult> GetListAsync()
    {
        var userId = Guid.Parse(User.Identity!.Name!);
        var excludedTrackList = await _userPremanager.GetExcludedTrackListAsync(userId);

        return Ok(excludedTrackList);
    }
}