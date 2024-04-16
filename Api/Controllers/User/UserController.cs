using Api.Controllers.File.Dto.Request;
using Api.Controllers.User.Dto.Request;
using Api.Controllers.User.Dto.Response;
using Api.Premanager.User;
using Dal.BaseUser.Repository;
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
    private readonly IUserRepository _userRepository;

    /// <inheritdoc />
    public UserController(IUserManager userManager, IUserPremanager userPremanager, IUserRepository userRepository)
    {
        _userManager = userManager;
        _userPremanager = userPremanager;
        _userRepository = userRepository;
    }

    /// <summary>
    /// Получить информацию о пользователе
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(GetUserResponse), 200)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAtLeastUser)]
    public async Task<IActionResult> GetAsync()
    {
        var userId = Guid.Parse(User.Identity!.Name!);
        var response = await _userPremanager.GetAsync(userId);

        return Ok(response);
    }

    /// <summary>
    /// Получить лого
    /// </summary>
    [HttpGet("{userId:guid}/logo")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetLogoAsync(
        [FromRoute] Guid userId)
        // [FromQuery] int? width,
        // [FromQuery] int? height,
        // [FromServices] ILogoResizeService logoResizeService)
    {
        // if (!width.HasValue && !height.HasValue)
        // {
        //     throw new InvalidImageSizeException();
        // }

        var logo = await _userManager.GetLogoAsync(userId);
        // var resized = await logoResizeService.ResizeAsync(logo, width, height);
        var contentType = ContentTypeHelper.GetContentTypeByFileExtension(logo.Extension);

        return File(logo.Content, contentType);
    }

    /// <summary>
    /// Обновить логотип
    /// </summary>
    [HttpPatch("logo")]
    [ProducesResponseType(204)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAtLeastUser)]
    public async Task<IActionResult> UpdateLogoAsync([FromForm] UploadFileRequest logoFile)
    {
        var userId = Guid.Parse(User.Identity!.Name!);
        await _userPremanager.UpdateLogoAsync(userId, logoFile);

        return NoContent();
    }

    /// <summary>
    /// Обновить информацию о пользователе
    /// </summary>
    [HttpPatch]
    [ProducesResponseType(204)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAtLeastUser)]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateUserNameRequest request)
    {
        var userId = Guid.Parse(User.Identity!.Name!);
        await _userManager.UpdateUserNameAsync(userId, request.UserName);

        return NoContent();
    }
}