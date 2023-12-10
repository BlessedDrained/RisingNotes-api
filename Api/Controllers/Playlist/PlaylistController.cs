using Api.Controllers.File.Dto.Request;
using Api.Controllers.Playlist.Dto.Request;
using Api.Controllers.Playlist.Dto.Response;
using Api.Premanager.Playlist;
using Dal.Playlist.Repository;
using Logic.Logo;
using Logic.Playlist;
using MainLib.Api.Auth.Constant;
using MainLib.Api.Controller;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RisingNotesLib.Exceptions;
using RisingNotesLib.Helper;

namespace Api.Controllers.Playlist;

/// <summary>
/// Контроллер для плейлистов
/// </summary>
[Route("playlist")]
public class PlaylistController : PublicController
{
    private readonly IPlaylistPremanager _playlistPremanager;
    private readonly IPlaylistManager _playlistManager;

    ///
    public PlaylistController(
        IPlaylistPremanager playlistPremanager,
        IPlaylistManager playlistManager)
    {
        _playlistPremanager = playlistPremanager;
        _playlistManager = playlistManager;
    }

    /// <summary>
    /// Создать плейлист
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(CreatePlaylistResponse), 201)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAtLeastUser)]
    public async Task<IActionResult> CreateAsync([FromForm] CreatePlaylistRequest request)
    {
        var userId = Guid.Parse(User.Identity!.Name!);
        var response = await _playlistPremanager.CreateAsync(request, userId);

        return CreatedAtAction("GetInfo", new {playlistId = response.Id}, response);
    }

    /// <summary>
    /// Получить список плейлистов пользователя
    /// </summary>
    /// <returns></returns>
    [HttpGet("list/{userId:guid}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetUserPlaylistListAsync([FromRoute] Guid userId)
    {
        var response = await _playlistPremanager.GetUserPlaylistInfoListAsync(userId);

        return Ok(response);
    }

    /// <summary>
    /// Получить информацию о плейлисте
    /// </summary>
    /// <remarks>Не отдает список треков</remarks>
    [HttpGet("{playlistId:guid}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetInfoAsync([FromRoute] Guid playlistId)
    {
        var response = await _playlistPremanager.GetInfoAsync(playlistId);

        return Ok(response);
    }

    /// <summary>
    /// Получить лого
    /// </summary>
    [HttpGet("{playlistId:guid}/logo")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetLogoAsync(
        [FromRoute] Guid playlistId,
        [FromQuery] int? width,
        [FromQuery] int? height,
        [FromServices] ILogoResizeService logoResizeService)
    {
        if (!width.HasValue && !height.HasValue)
        {
            throw new InvalidImageSizeException();
        }

        var file = await _playlistManager.GetLogoAsync(playlistId);
        var resized = await logoResizeService.ResizeAsync(file, width, height);
        var contentType = ContentTypeHelper.GetContentTypeByFileExtension(file.Extension);

        return File(resized, contentType);
    }

    /// <summary>
    /// Получить список треков плейлиста
    /// </summary>
    [HttpGet("{playlistId:guid}/song/list")]
    [ProducesResponseType(typeof(GetPlaylistSongListResponse), 200)]
    public async Task<IActionResult> GetTrackListAsync([FromRoute] Guid playlistId)
    {
        var response = await _playlistPremanager.GetSongListAsync(playlistId);

        return Ok(response);
    }

    /// <summary>
    /// Добавить трек в плейлист
    /// </summary>
    [HttpPatch("{playlistId:guid}/song/{songId:guid}")]
    [ProducesResponseType(204)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAtLeastUser)]
    public async Task<IActionResult> AddToPlaylistAsync([FromRoute] Guid playlistId, [FromRoute] Guid songId)
    {
        // TODO: сделать проверку, что плейлист принадлежит пользователю, от имени которого отправлен запрос
        await _playlistManager.AddTrackAsync(playlistId, songId);

        return Ok();
    }

    /// <summary>
    /// Обновить плейлист
    /// </summary>
    /// <returns></returns>
    [HttpPatch("{playlistId:guid}")]
    [ProducesResponseType(204)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAtLeastUser)]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid playlistId, [FromBody] UpdatePlaylistRequest request)
    {
        await _playlistPremanager.UpdateAsync(playlistId, request);
        
        return NoContent();
    }
    
    /// <summary>
    /// Удалить плейлист
    /// </summary>
    [HttpDelete("{playlistId:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAtLeastUser)]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] Guid playlistId,
        [FromServices] IPlaylistRepository playlistRepository)
    {
        await playlistRepository.DeleteAsync(playlistId);

        return NoContent();
    }

    /// <summary>
    /// Обновить логотип
    /// </summary>
    [HttpPatch("{playlistId:guid}/logo")]
    [ProducesResponseType(204)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAtLeastUser)]
    public async Task<IActionResult> UpdateLogoAsync([FromRoute] Guid playlistId, [FromForm] UploadFileRequest logoFile)
    {
        var userId = Guid.Parse(User.Identity!.Name!);
        await _playlistPremanager.UpdateLogoAsync(userId, playlistId, logoFile);

        return NoContent();
    }

    /// <summary>
    /// Получить список плейлистов по фильтрам
    /// </summary>
    [HttpGet("list")]
    [ProducesResponseType(typeof(GetPlaylistListResponse), 200)]
    public async Task<IActionResult> GetListAsync([FromQuery] GetPlaylistListRequest request)
    {
        Guid? userId;
        if (User.Identity!.Name == null)
        {
            userId = null;
        }
        else
        {
            userId = Guid.Parse(User.Identity.Name);
        }
        
        var response = await _playlistPremanager.GetListAsync(userId, request);

        return Ok(response);
    }
}