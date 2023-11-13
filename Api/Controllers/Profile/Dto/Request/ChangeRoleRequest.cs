namespace Api.Controllers.Profile.Dto.Request;

/// <summary>
/// Запрос на обновление роли
/// </summary>
public record ChangeRoleRequest
{
    /// <summary>
    /// Новая роль
    /// </summary>
    public string NewRoleName { get; init; }
}