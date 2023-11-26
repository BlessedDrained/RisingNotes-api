using System.Numerics;
using Api.Controllers.File.Dto.Request;
using Api.Premanager.User;
using Dal.Author;
using Logic.Logo;
using Logic.User;
using MainLib.Api.Auth.Constant;
using MainLib.Api.Controller;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RisingNotesLib.Exceptions;
using RisingNotesLib.Helper;

namespace Api.Controllers.User;

/// <summary>
/// Общий для всех пользователей функционал
/// </summary>
[Route("user")]
public class UserController : PublicController
{
    private readonly IUserManager _userManager;
    private readonly IUserPremanager _userPremanager;

    /// <inheritdoc />
    public UserController(IUserManager userManager, IUserPremanager userPremanager)
    {
        _userManager = userManager;
        _userPremanager = userPremanager;
    }

    /// <summary>
    /// Получить лого
    /// </summary>
    [HttpGet("{userId:guid}/logo")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetLogoAsync(
        [FromRoute] Guid userId,
        [FromQuery] int? width,
        [FromQuery] int? height,
        [FromServices] ILogoResizeService logoResizeService)
    {
        if (!width.HasValue && !height.HasValue)
        {
            throw new InvalidImageSizeException();
        }

        var logo = await _userManager.GetLogoAsync(userId);
        var resized = await logoResizeService.ResizeAsync(logo, width, height);
        var contentType = ContentTypeHelper.GetContentTypeByFileExtension(logo.Extension);

        return File(resized, contentType);
    }

    /// <summary>
    /// Обновить логотип
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpPatch("logo")]
    [ProducesResponseType(204)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAtLeastUser)]
    public async Task<IActionResult> UpdateLogoAsync([FromForm] UploadFileRequest logoFile)
    {
        var userId = Guid.Parse(User.Identity!.Name!);
        await _userPremanager.UpdateLogoAsync(userId, logoFile);

        return NoContent();
    }
}