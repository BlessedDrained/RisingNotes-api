using Logic.Logo;
using Logic.User;
using MainLib.Api.Controller;
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

    /// <inheritdoc />
    public UserController(IUserManager userManager)
    {
        _userManager = userManager;
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
}