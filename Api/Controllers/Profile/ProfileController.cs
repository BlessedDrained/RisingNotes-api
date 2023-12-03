using Api.Controllers.Profile.Dto.Request;
using Api.Controllers.Profile.Dto.Response;
using Api.Premanager.Auth;
using MainLib.Api.Auth.Constant;
using MainLib.Api.Controller;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Profile;

/// <summary>
/// Контроллер аутентификации и авторизации
/// </summary>
[Route("profile")]
public class ProfileController : PublicController
{
    private readonly IProfilePremanager _profilePremanager;

    /// <summary>
    /// Конструктор
    /// </summary>
    public ProfileController(IProfilePremanager profilePremanager)
    {
        _profilePremanager = profilePremanager;
    }

    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    [HttpPost("registration")]
    [ProducesResponseType(201)]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest registerRequest)
    {
        await _profilePremanager.RegisterAsync(registerRequest);

        return Created("", null);
    }

    /// <summary>
    /// Получить информацию профиля пользователя
    /// </summary>
    [HttpGet("{identityUserId:guid}")]
    [ProducesResponseType(typeof(GetProfileResponse), 200)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAtLeastUser)]
    public async Task<IActionResult> GetProfileAsync([FromRoute] Guid identityUserId)
    {
        var response = await _profilePremanager.GetProfileAsync(identityUserId);

        return Ok(response);
    }
}