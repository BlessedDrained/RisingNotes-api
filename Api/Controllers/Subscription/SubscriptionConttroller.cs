using Api.Controllers.Subscription.Dto.Response;
using Api.Premanager.Author;
using Api.Premanager.User;
using Logic.User;
using MainLib.Api.Auth.Constant;
using MainLib.Api.Controller;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Subscription;

/// <summary>
/// Контроллер для подписок на авторов
/// </summary>
[Route("subscription")]
public class SubscriptionController : PublicController
{
    private readonly IUserPremanager _userPremanager;
    private readonly IUserManager _userManager;
    private readonly IAuthorPremanager _authorPremanager;

    public SubscriptionController(IUserPremanager userPremanager, IUserManager userManager, IAuthorPremanager authorPremanager)
    {
        _userPremanager = userPremanager;
        _userManager = userManager;
        _authorPremanager = authorPremanager;
    }

    /// <summary>
    /// Подписаться на автора
    /// </summary>
    [HttpPost("{authorId:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAtLeastUser)]
    public async Task<IActionResult> SubscribeAsync([FromRoute] Guid authorId)
    {
        var userId = Guid.Parse(User.Identity!.Name!);
        await _userManager.SubscribeAsync(userId, authorId);

        return NoContent();
    }

    /// <summary>
    /// Отписаться от автора
    /// </summary>
    [HttpDelete("{authorId:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAtLeastUser)]
    public async Task<IActionResult> UnsubscribeAsync([FromRoute] Guid authorId)
    {
        var userId = Guid.Parse(User.Identity!.Name!);
        await _userManager.UnsubscribeAsync(userId, authorId);

        return NoContent();
    }

    /// <summary>
    /// Получить список подписок пользователя
    /// </summary>
    [HttpGet("{userId:guid}/list")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PolicyConstant.RequireAtLeastUser)]
    [ProducesResponseType(typeof(GetSubscriptionListResponse), 200)]
    public async Task<IActionResult> GetListAsync([FromRoute] Guid userId)
    {
        var subList = await _userPremanager.GetSubscriptionListAsync(userId);

        return Ok(subList);
    }

    /// <summary>
    /// Получить количество подписчиков автора
    /// </summary>
    [HttpGet("{authorId:guid}/count")]
    [ProducesResponseType(typeof(GetSubscriberCountResponse), 200)]
    public async Task<IActionResult> GetCountAsync([FromRoute] Guid authorId)
    {
        var count = await _authorPremanager.GetSubscriberCountAsync(authorId);

        return Ok(count);
    }
}