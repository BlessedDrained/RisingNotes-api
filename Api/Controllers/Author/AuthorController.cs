using Api.Controllers.Author.Dto.Request;
using Api.Controllers.Author.Dto.Response;
using Api.Controllers.Song.Dto.Response;
using Api.Premanager.Author;
using Api.Premanager.Music;
using MainLib.Api.Auth.Constant;
using MainLib.Api.Controller;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Author;

/// <summary>
/// Контроллер для автора
/// </summary>
[Route("author")]
public class AuthorController : PublicController
{
    private readonly IAuthorPremanager _authorPremanager;

    /// 
    public AuthorController(IAuthorPremanager authorPremanager)
    {
        _authorPremanager = authorPremanager;
    }

    /// <summary>
    /// Сделать пользователя автором
    /// </summary>
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAdmin)]
    public async Task<IActionResult> MakeAuthorAsync([FromBody] MakeAuthorRequest request)
    {
        var id = await _authorPremanager.MakeAuthorAsync(request);

        return Created(string.Empty, new {id});
    }

    /// <summary>
    /// Получить информацию об авторе
    /// </summary>
    /// <remarks>Не отдает информацию о треках</remarks>
    [HttpGet("{authorName}")]
    [ProducesResponseType(typeof(GetAuthorInfoResponse), 200)]
    public async Task<IActionResult> GetInfoAsync([FromRoute] string authorName)
    {
        var response = await _authorPremanager.GetInfoAsync(authorName);

        return Ok(response);
    }

    /// <summary>
    /// Получить краткую информацию об авторе
    /// </summary>
    /// <remarks>Не отдает информацию о треках</remarks>
    [HttpGet("{authorName}/short-info")]
    [ProducesResponseType(typeof(GetAuthorShortInfoResponse), 200)]
    [Obsolete("Пусть будет. Мб выпилю.")]
    public async Task<IActionResult> GetShortInfoAsync([FromRoute] string authorName)
    {
        var response = await _authorPremanager.GetInfoAsync(authorName);
    
        return Ok(response);
    }

    /// <summary>
    /// Получить информацию об авторе
    /// </summary>
    /// <remarks>Не отдает информацию о треках</remarks>
    [HttpGet("{authorId:guid}")]
    [ProducesResponseType(typeof(GetAuthorInfoResponse), 200)]
    public async Task<IActionResult> GetInfoAsync([FromRoute] Guid authorId)
    {
        var response = await _authorPremanager.GetInfoAsync(authorId);

        return Ok(response);
    }

    /// <summary>
    /// Получить список авторов по фильтрам
    /// </summary>
    /// <returns></returns>
    [HttpGet("list")]
    [ProducesResponseType(typeof(GetAuthorListResponse), 200)]
    public async Task<IActionResult> GetListAsync([FromQuery] GetAuthorListRequest request)
    {
        var response = await _authorPremanager.GetListAsync(request);

        return Ok(response);
    }

    /// <summary>
    /// Получить список треков автора
    /// </summary>
    [HttpGet("{authorId:guid}/song/list")]
    [ProducesResponseType(typeof(GetAuthorSongInfoListResponse), 200)]
    public async Task<IActionResult> GetSongListAsync(
        [FromRoute] Guid authorId,
        [FromServices] ISongPremanager songPremanager)
    {
        var response = await songPremanager.GetAuthorSongInfoListAsync(authorId);

        return Ok(response);
    }
}