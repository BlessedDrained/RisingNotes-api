using Api.Controllers.Song.Dto.Response;
using Api.Premanager.Music;
using Logic.Song;
using MainLib.Api.Auth.Constant;
using MainLib.Api.Controller;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.FavoriteSong;

/// <summary>
/// Контроллер для избранных треков
/// </summary>
[Route("song/favorite")]
public class FavoriteSongController : PublicController
{
    /// <summary>
    /// Добавить трек в избранное
    /// </summary>
    [HttpPatch("{songId:guid}")]
    [ProducesResponseType(204)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAtLeastUser)]
    public async Task<IActionResult> AddToFavoriteAsync(
        [FromRoute] Guid songId, 
        [FromServices] ISongManager songManager)
    {
        var userId = Guid.Parse(User.Identity!.Name!);
        await songManager.AddFavoriteAsync(userId, songId);

        return NoContent();
    }

    /// <summary>
    /// Удалить трек из избранного
    /// </summary>
    [ProducesResponseType(204)]
    [HttpDelete("{songId:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAtLeastUser)]
    public async Task<IActionResult> RemoveFromFavoriteAsync(
        [FromRoute] Guid songId, 
        [FromServices] ISongManager songManager)
    {
        var userId = Guid.Parse(User.Identity!.Name!);
        await songManager.RemoveFavoriteAsync(userId, songId);

        return NoContent();
    }
    
    /// <summary>
    /// Получить список информации об избранных треках
    /// </summary>
    [HttpGet("favorite/list")]
    [ProducesResponseType(typeof(GetFavoriteSongInfoListResponse), 200)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAtLeastUser)]
    public async Task<IActionResult> GetFavoriteSongInfoListAsync([FromServices] ISongPremanager songPremanager)
    {
        var userId = User.Identity!.Name;
        var response = await songPremanager.GetFavoriteSongInfoList(Guid.Parse(userId!));

        return Ok(response);
    }
}