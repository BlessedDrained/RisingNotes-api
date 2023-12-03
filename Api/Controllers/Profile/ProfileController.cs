using Api.Controllers.Profile.Dto.Request;
using Api.Premanager.Auth;
using MainLib.Api.Controller;
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
}