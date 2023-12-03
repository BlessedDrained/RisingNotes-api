using Api.Controllers.Author.Dto.Request;
using Api.Controllers.Author.Dto.Response;
using Api.Controllers.Song.Dto.Response;
using Api.Premanager.Author;
using Api.Premanager.Music;
using Dal.Author.Repository;
using Logic.Logo;
using Logic.User;
using MainLib.Api.Auth.Constant;
using MainLib.Api.Controller;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RisingNotesLib.Constant;
using RisingNotesLib.Exceptions;
using RisingNotesLib.Helper;

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

    /// <summary>
    /// Обновить информацию о музыканте
    /// </summary>
    [HttpPatch("{authorId:guid}")]
    [ProducesResponseType(204)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAtLeastAuthor)]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid authorId, [FromBody] UpdateAuthorRequest request)
    {
        var authorIdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypeConstants.AuthorId);
        if (Guid.Parse(authorIdClaim.Value) != authorId)
        {
            return BadRequest();
        }

        await _authorPremanager.UpdateAsync(authorId, request);

        return NoContent();
    }

    /// <summary>
    /// Получить лого
    /// </summary>
    [HttpGet("{authorId:guid}/logo")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetLogoAsync(
        [FromRoute] Guid authorId,
        [FromQuery] int? width,
        [FromQuery] int? height,
        [FromServices] ILogoResizeService logoResizeService,
        [FromServices] IAuthorRepository authorRepository,
        [FromServices] IUserManager userManager)
    {
        if (!width.HasValue && !height.HasValue)
        {
            throw new InvalidImageSizeException();
        }

        var author = await authorRepository.GetAsync(authorId);
        var logo = await userManager.GetLogoAsync(author.UserId);
        var resized = await logoResizeService.ResizeAsync(logo, width, height);
        var contentType = ContentTypeHelper.GetContentTypeByFileExtension(logo.Extension);

        return File(resized, contentType);
    }
}