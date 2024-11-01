﻿using Api.Controllers.File.Dto.Request;
using Api.Controllers.Song.Dto.Request;
using Api.Controllers.Song.Dto.Response;
using Api.Premanager.Music;
using Dal.Song;
using Logic.Song;
using MainLib.Api.Auth.Constant;
using MainLib.Api.Controller;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RisingNotesLib.Constant;
using RisingNotesLib.Helper;

namespace Api.Controllers.Song;

/// <summary>
/// Контроллер для <see cref="SongDal"/>
/// </summary>
[Route("song")]
public class SongController : PublicController
{
    private readonly ISongPremanager _songPremanager;
    private readonly ISongManager _songManager;

    /// <inheritdoc />
    public SongController(
        ISongPremanager musicPremanager,
        ISongManager songManager)
    {
        _songPremanager = musicPremanager;
        _songManager = songManager;
    }

    /// <summary>
    /// Добавить трек
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(UploadSongResponse), 200)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAtLeastAuthor)]
    public async Task<IActionResult> UploadAsync([FromForm] UploadSongRequest request)
    {
        var claim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypeConstants.AuthorId);
        if (claim == null)
        {
            return Unauthorized();
        }

        var authorId = Guid.Parse(claim.Value);
        var response = await _songPremanager.CreateAsync(request, authorId);

        return CreatedAtAction("GetInfo", new {songId = response.Id}, response);
    }

    /// <summary>
    /// Получить информацию о треке
    /// </summary>
    [HttpGet("{songId:guid}")]
    [ProducesResponseType(typeof(GetWithAuthorSongInfoResponse), 200)]
    public async Task<IActionResult> GetInfoAsync([FromRoute] Guid songId)
    {
        var response = await _songPremanager.GetSongInfoAsync(songId);

        return Ok(response);
    }
    
    /// <summary>
    /// Получить файл с треком
    /// </summary>
    [HttpGet("{songId:guid}/file")]
    [ProducesResponseType(200)]
    [ProducesResponseType(206)]
    public async Task<IActionResult> GetFileAsync([FromRoute] Guid songId)
    {
        var file = await _songManager.GetSongFileAsync(songId);
        var contentType = ContentTypeHelper.GetContentTypeByFileExtension(file.Extension);
        
        return File(file.Content, contentType, enableRangeProcessing: true);
    }

    /// <summary>
    /// Получить лого трека
    /// </summary>
    [HttpGet("{songId:guid}/logo")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetLogoAsync(
        [FromRoute] Guid songId)
    {
        var logo = await _songManager.GetSongLogoAsync(songId);
        var contentType = ContentTypeHelper.GetContentTypeByFileExtension(logo.Extension);

        return File(logo.Content, contentType);
    }

    /// <summary>
    /// Получить список песен по фильтрам
    /// </summary>
    [HttpGet("list")]
    [ProducesResponseType(typeof(GetSongListResponse), 200)]
    public async Task<IActionResult> GetListAsync([FromQuery] GetSongListRequest request)
    {
        var response = await _songPremanager.GetSongListAsync(request);

        return Ok(response);
    }

    /// <summary>
    /// Обновить логотип
    /// </summary>
    [HttpPatch("{songId:guid}/logo")]
    [ProducesResponseType(204)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAtLeastAuthor)]
    public async Task<IActionResult> UpdateLogoAsync([FromRoute] Guid songId, [FromForm] UploadFileRequest logoFile)
    {
        var claim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypeConstants.AuthorId);
        if (claim == null)
        {
            return Unauthorized();
        }

        var authorId = Guid.Parse(claim.Value);
        
        await _songPremanager.UpdateLogoAsync(authorId, songId, logoFile);

        return NoContent();
    }


    /// <summary>
    /// Получить количество прослушиваний песни
    /// </summary>
    [HttpGet("{songId:guid}/audition/count")]
    [ProducesResponseType(typeof(GetAuditionCountResponse), 200)]
    public async Task<IActionResult> GetAuditionCountAsync([FromRoute] Guid songId)
    {
        var response = await _songPremanager.GetAuditionCountAsync(songId);

        return Ok(response);
    }
}