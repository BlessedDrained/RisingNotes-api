namespace Api.Controllers.Profile.Dto.Response;

/// <summary>
/// Ответ на регистрацию
/// </summary>
public record RegisterResponse
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; init; }
    
    /// <summary>
    /// Идентификатор пользователя Identity
    /// </summary>
    public Guid IdentityUserGuid { get; init; }
}